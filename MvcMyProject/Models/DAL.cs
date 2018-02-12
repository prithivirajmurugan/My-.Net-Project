using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace MvcMyProject.Models
{
    public class DAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public bool addCustomers(UserModel obj)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            SqlCommand cmd_insert = new SqlCommand("insert customer values(@customername,@customermail,@city,@mobile,@dateofbirth,@gender)", con);
            cmd_insert.Parameters.AddWithValue("@customername", obj.userName);
            cmd_insert.Parameters.AddWithValue("@customermail", obj.EmailId);
            cmd_insert.Parameters.AddWithValue("@city",Convert.ToInt32(obj.userCityID));
            cmd_insert.Parameters.AddWithValue("@mobile", obj.mobileNumber);
            cmd_insert.Parameters.AddWithValue("@dateofbirth", obj.dateOfBirth);
            cmd_insert.Parameters.AddWithValue("@gender", obj.gender);
            cmd_insert.Transaction = tran;
            cmd_insert.ExecuteNonQuery();
            MembershipCreateStatus status;
            Membership.CreateUser(obj.EmailId, obj.password, obj.EmailId, obj.questionName, obj.answer, true, out status);
            if (status == MembershipCreateStatus.Success)
            {

                if(obj.userType=="2")
                Roles.AddUserToRole(obj.EmailId, "Owner");
                if (obj.userType == "1")
                    Roles.AddUserToRole(obj.EmailId, "Customer");
                tran.Commit();
                con.Close();
                return true;
            }
            else
            {
                tran.Rollback();
                con.Close();
                return false;
            }

        }
        public List<CityModel> getCities()
        {
            con.Open();
            SqlCommand cmd_get_cities = new SqlCommand("select * from city", con);
            SqlDataReader dr_cities = cmd_get_cities.ExecuteReader();
            List<CityModel> list_cities = new List<CityModel>();
            while (dr_cities.Read())
            {
                CityModel obj = new CityModel();
                obj.cityID = dr_cities.GetInt32(0);
                obj.cityName = dr_cities.GetString(1);
                list_cities.Add(obj);
 
            }
            con.Close();
            return list_cities;
        }
        public List<string> ls_questions()
        {
            List<string> questions = new List<string>();
            return null;
        }
        public bool login(LoginModel obj)
        {
            con.Open();
            if (Membership.ValidateUser(obj.emailID, obj.password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public int getCityid(string cityname)
        {
            con.Open();
            SqlCommand cmd_getcityid = new SqlCommand("select cityid from city where cityname=@cityname", con);
            cmd_getcityid.Parameters.AddWithValue("@cityname", cityname);
            int cityid = Convert.ToInt32(cmd_getcityid.ExecuteScalar());
            con.Close();
            return cityid;
        }

        public List<PgModel> get_list_pg_on_cities(string cityname)
        {
            con.Open();
            int cityid = getCityid(cityname);
            SqlCommand cmd_get_pg_on_cities = new SqlCommand("Select * from pgtable where pgcity=@cityid", con);
            cmd_get_pg_on_cities.Parameters.AddWithValue("@cityid", cityid);
            SqlDataReader dr_pgs = cmd_get_pg_on_cities.ExecuteReader();
            List<PgModel> ls = new List<PgModel>();
            while (dr_pgs.Read())
            {
                PgModel pg = new PgModel();
                pg.pgID = dr_pgs.GetInt32(0);
                pg.pgName = dr_pgs.GetString(2);
                pg.pgaddress = dr_pgs.GetString(3);
                pg.nearbyarea = dr_pgs.GetString(4);
                pg.pgImagesAddress = dr_pgs.GetString(6);
                pg.amenities = dr_pgs.GetString(7);
                ls.Add(pg);
                
            }
            con.Close();
            return ls;
            
        }
        public List<PgModel> get_list_pg_on_nearbyarea(string cityname, string nearbyarea)
        {
            int cityid = getCityid(cityname);
            con.Open();
            SqlCommand cmd_pg = new SqlCommand("Select * from pgtable where pgcity=@cityid and pgnearbyarea=@nearby", con);
            cmd_pg.Parameters.AddWithValue("@cityid", cityid);
            cmd_pg.Parameters.AddWithValue("@nearby", nearbyarea);
            SqlDataReader dr_pg = cmd_pg.ExecuteReader();
            List<PgModel> list = new List<PgModel>();
            while (dr_pg.Read())
            {
                PgModel obj = new PgModel();
                obj.pgID = dr_pg.GetInt32(0);
                obj.OBuserID = dr_pg.GetString(1);
                obj.pgName = dr_pg.GetString(2);
                obj.pgaddress = dr_pg.GetString(3);
                obj.nearbyarea = dr_pg.GetString(4);
                obj.pgcityid = dr_pg.GetInt32(5).ToString();
                obj.pgImagesAddress = dr_pg.GetString(6);
                obj.amenities = dr_pg.GetString(7);
                obj.pgDescription = dr_pg.GetString(8);
                list.Add(obj);
                
            }
            con.Close();
            return list;
        }
        public List<string> getnearbyarea(string cityname)
        {
            con.Open();
            SqlCommand cmd_getcityid = new SqlCommand("select cityid from city where cityname=@cityname", con);
            cmd_getcityid.Parameters.AddWithValue("@cityname", cityname);
            int cityid = Convert.ToInt32(cmd_getcityid.ExecuteScalar());
            SqlCommand cmd_getnearbycities = new SqlCommand("select distinct pgnearbyarea  from pgtable where pgcity=@cityid", con);
            cmd_getnearbycities.Parameters.AddWithValue("@cityid", cityid);
            SqlDataReader dr_nearbyarea = cmd_getnearbycities.ExecuteReader();
            List<string> ls = new List<string>();
            while(dr_nearbyarea.Read())
            {
                string s = dr_nearbyarea.GetString(0);
                ls.Add(s);
                
 
            }
            return ls;
 
        }
        public bool addtofavourites(string userid, int pgid, int roomid)
        {
            int count = 0;
            con.Open();
            SqlCommand cmd_check_fav = new SqlCommand("select count(*) from favourites where userid=@userid and pgid=@pgid and roomid=@roomid", con);
            cmd_check_fav.Parameters.AddWithValue("@userid", userid);
            cmd_check_fav.Parameters.AddWithValue("@pgid", pgid);
            cmd_check_fav.Parameters.AddWithValue("@roomid", roomid);
            count = Convert.ToInt32(cmd_check_fav.ExecuteScalar());
            if (count >= 1)
            {
                return true;
            }
            SqlCommand cmd_insert_add = new SqlCommand("insert favourites values (@userid,@pgid,@roomid)", con);
            cmd_insert_add.Parameters.AddWithValue("@userid", userid);
            cmd_insert_add.Parameters.AddWithValue("@pgid", pgid);
            cmd_insert_add.Parameters.AddWithValue("@roomid", roomid);
            
            count = Convert.ToInt32(cmd_insert_add.ExecuteNonQuery());
            if (count > 0)
            {
                return true;
            }
            else {
                return false;
            }

        }
        public void deletefromfavourites(string pgid, string roomid)
        {
            con.Open();
            SqlCommand cmd_del = new SqlCommand("delete from favourites where pgid=@pgid and roomid=@roomid", con);
            cmd_del.Parameters.AddWithValue("@pgid", pgid);
            cmd_del.Parameters.AddWithValue("@roomid", pgid);
            cmd_del.ExecuteNonQuery();
        }
        public List<FavouritesModel> showfavourites(string userid)
        {
            con.Open();
            SqlCommand cmd_showfav = new SqlCommand("select pgtable.pgname,pgtable.pgaddress,roomstable.roomname,roomstable.roomavailability,pgtable.pgid,roomstable.roomid,roomstable.roomrent from pgtable join roomstable on pgtable.pgID=roomstable.pgid where pgtable.pgid in (select pgtable.pgid from favourites where userid=@userid) and roomstable.roomID in (select roomid from favourites where userid=@userid)", con);
            cmd_showfav.Parameters.AddWithValue("@userid", userid);
            SqlDataReader dr_fav = cmd_showfav.ExecuteReader();
            List<FavouritesModel> ls=new List<FavouritesModel>();
            while (dr_fav.Read())
            {
                FavouritesModel obj = new FavouritesModel();
                obj.pgName = dr_fav.GetString(0);
                obj.pgaddress = dr_fav.GetString(1);
                obj.roomname = dr_fav.GetString(2);
                obj.roomavailability = dr_fav.GetString(3);
                obj.pgID = dr_fav.GetInt32(4);
                obj.roomID = dr_fav.GetInt32(5);
                obj.roomrent = dr_fav.GetInt32(6);
                ls.Add(obj);
 
            }
            return ls;
        }
        public RoomModel getFavDetails(int pgid, int roomid)
        {
            con.Open();
            SqlCommand cmd_getfavrooms = new SqlCommand("select roomstable.roomname ,roomstable.roomavailability,roomstable.roomoccupancy,roomstable.roomamenities,roomstable.roomrent,roomImages.roomimage1,roomImages.roomimage2 from roomstable join roomImages on roomstable.roomID=roomImages.roomid where pgid=@pgid and roomstable.roomID=@roomid", con);
            cmd_getfavrooms.Parameters.AddWithValue("@pgid", pgid);
            cmd_getfavrooms.Parameters.AddWithValue("@roomid", roomid);
            SqlDataReader dr_favdetails = cmd_getfavrooms.ExecuteReader();
            RoomModel obj = new RoomModel();
            if (dr_favdetails.Read())
            {
                obj.roomname = dr_favdetails.GetString(0);
                obj.roomavailability = dr_favdetails.GetString(1);
                obj.roomoccupancy = dr_favdetails.GetInt32(2);
                obj.roomamenities = dr_favdetails.GetString(3);
                obj.roomrent = dr_favdetails.GetInt32(4);
                obj.roomimage1 = dr_favdetails.GetString(5);
                obj.roomimage2 = dr_favdetails.GetString(6);
 
            }
            return obj;
        }



        public bool checkEmail(string emailid)
        {
            int countcust=0,countown=0;
            con.Open();
            SqlCommand cmd_checkemailcust = new SqlCommand("select count(*) from customer where customeremail=@emailid", con);
            cmd_checkemailcust.Parameters.AddWithValue("@emailid",emailid);
           countcust= Convert.ToInt32(cmd_checkemailcust.ExecuteScalar());
            SqlCommand cmd_checkemailown = new SqlCommand("select count(*) from owner where owneremail=@emailid1", con);
            cmd_checkemailown.Parameters.AddWithValue("@emailid1", emailid);
            countown = Convert.ToInt32(cmd_checkemailown.ExecuteScalar());
            con.Close();
            return !(countcust > 0 || countown > 0);
        }
    }



}

    
