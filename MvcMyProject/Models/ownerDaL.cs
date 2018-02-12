using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.Mvc;
using MvcMyProject.Models;

namespace MvcMyProject.Models
{
    public class ownerDaL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
           public bool addOwner(UserModel model)
        {
            
            con.Open();
            SqlTransaction tran=con.BeginTransaction();
            SqlCommand cmd_insert_owner = new SqlCommand("insert owner values(@ownername,@ownermail,@ownercity,@mobile,@dob,@gender)", con);
            cmd_insert_owner.Parameters.AddWithValue("@ownername", model.userName);
            cmd_insert_owner.Parameters.AddWithValue("@ownermail", model.EmailId);
            cmd_insert_owner.Parameters.AddWithValue("@ownercity", model.userCityID);
            cmd_insert_owner.Parameters.AddWithValue("@mobile", model.mobileNumber);
            cmd_insert_owner.Parameters.AddWithValue("@dob", model.dateOfBirth);
            cmd_insert_owner.Parameters.AddWithValue("@gender", model.gender);
            cmd_insert_owner.Transaction = tran;
            cmd_insert_owner.ExecuteNonQuery();
            MembershipCreateStatus status;
            Membership.CreateUser(model.EmailId,model.password,model.EmailId,model.questionName,model.answer,true,out status);
            if (status == MembershipCreateStatus.Success)
            {
                Roles.AddUserToRole(model.EmailId,"Owner");
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

           public bool addPg(PgModel model)
           {
               
                   con.Open();
                   SqlTransaction tran = con.BeginTransaction();
                   SqlCommand cmd_insert_pg = new SqlCommand("insert pgtable values(@obuserid,@pgname,@pgaddress,@pgnearbyarea,@pgcity,null,@pgAmenities,@pgdescription)", con);
                   cmd_insert_pg.Parameters.AddWithValue("@pgname", model.pgName);
                   cmd_insert_pg.Parameters.AddWithValue("@obuserid", model.OBuserID);
                   cmd_insert_pg.Parameters.AddWithValue("@pgaddress", model.pgaddress);
                   cmd_insert_pg.Parameters.AddWithValue("@pgnearbyarea", model.nearbyarea);
                   cmd_insert_pg.Parameters.AddWithValue("@pgcity", Convert.ToInt32(model.pgcityid));
                   cmd_insert_pg.Parameters.AddWithValue("@pgAmenities", model.amenities);
                   cmd_insert_pg.Parameters.AddWithValue("@pgdescription", model.pgDescription);
                   cmd_insert_pg.Transaction = tran;
                   cmd_insert_pg.ExecuteNonQuery();
                   SqlCommand cmd_pgid = new SqlCommand("select @@identity", con);
                   cmd_pgid.Transaction = tran;
                   model.pgID = Convert.ToInt32(cmd_pgid.ExecuteScalar());
                   model.pgImagesAddress = "/PGImages/" + model.pgID + ".jpg";
                   SqlCommand cmd_update_img = new SqlCommand(@"update pgtable set pgImageaddress=@pgimg where pgid=@pgid", con);
                   cmd_update_img.Parameters.AddWithValue("@pgimg", model.pgImagesAddress);
                   cmd_update_img.Parameters.AddWithValue("@pgid", model.pgID);
                   cmd_update_img.Transaction = tran;
                   cmd_update_img.ExecuteNonQuery();


                   if (model.pgID != 0)
                   {
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
           
            public PgModel getPgDetails(int id)
            {
                con.Open();
                SqlCommand cmd_get_pg=new SqlCommand("Select * from pgtable where pgID=@pgID",con);
                cmd_get_pg.Parameters.AddWithValue("@pgID",id);
                SqlDataReader dr_pg=cmd_get_pg.ExecuteReader();
                PgModel model=new PgModel();
                if(dr_pg.Read())
                {
                    model.pgID=dr_pg.GetInt32(0);
                    model.pgName=dr_pg.GetString(2);
                    model.pgaddress=dr_pg.GetString(3);
                    model.nearbyarea=dr_pg.GetString(4);
                    model.pgcityid=dr_pg.GetString(5);
                    model.pgImagesAddress=dr_pg.GetString(6);
                    model.amenities=dr_pg.GetString(7);
                }
                return model;
            }
            public List<PgModel> getPgofowner(string ownerid)
            {
                con.Open();
                SqlCommand cmd_get_pg_of_owner = new SqlCommand("Select * from pgtable where obuserid=@obuserid", con);
                cmd_get_pg_of_owner.Parameters.AddWithValue("@obuserid", ownerid);
                SqlDataReader dr_pgs_of_owner = cmd_get_pg_of_owner.ExecuteReader();
                List<PgModel> ls = new List<PgModel>();
                while(dr_pgs_of_owner.Read())
                {
                    PgModel obj = new PgModel();
                    obj.pgID = dr_pgs_of_owner.GetInt32(0);
                    obj.OBuserID = dr_pgs_of_owner.GetString(1);
                    obj.pgName = dr_pgs_of_owner.GetString(2);
                    obj.pgaddress = dr_pgs_of_owner.GetString(3);
                    obj.nearbyarea = dr_pgs_of_owner.GetString(4);
                    obj.pgcityid = dr_pgs_of_owner.GetInt32(5).ToString();
                    obj.pgImagesAddress = dr_pgs_of_owner.GetString(6);
                    obj.amenities = dr_pgs_of_owner.GetString(7);
                    obj.pgDescription = dr_pgs_of_owner.GetString(8);
                    ls.Add(obj);
                    
                }
                return ls;
            }
            public bool addRoom(RoomModel model)
            {
                con.Open();
                int count = 0;
                SqlCommand cmd_roomexists = new SqlCommand("Select count(*) from roomstable where pgid=@pgid and roomname=@roomname", con);
                cmd_roomexists.Parameters.AddWithValue("@pgid", model.pgID);
                cmd_roomexists.Parameters.AddWithValue("@roomname", model.roomname);
                count = Convert.ToInt32(cmd_roomexists.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                SqlTransaction tran = con.BeginTransaction();
                SqlCommand cmd_add_rooms = new SqlCommand("insert roomstable values (@pgId,@roomname,@roomavailability,@roomoccupancy,@roomamenities,@roomrent)", con);
                cmd_add_rooms.Parameters.AddWithValue("@pgID", model.pgID);
                cmd_add_rooms.Parameters.AddWithValue("@roomname", model.roomname);
                cmd_add_rooms.Parameters.AddWithValue("@roomavailability", model.roomavailability);
                cmd_add_rooms.Parameters.AddWithValue("@roomoccupancy", model.roomoccupancy);
                cmd_add_rooms.Parameters.AddWithValue("@roomamenities", model.roomamenities);
                cmd_add_rooms.Parameters.AddWithValue("@roomrent", model.roomrent);
                cmd_add_rooms.Transaction = tran;
                cmd_add_rooms.ExecuteNonQuery();
                SqlCommand cmd_roomid = new SqlCommand("select @@identity", con);
                cmd_roomid.Transaction = tran;
                model.roomID =Convert.ToInt32( cmd_roomid.ExecuteScalar());
                model.roomimage1 = "/RoomImages/" + model.roomID + "1.jpg";
                model.roomimage2 = "/RoomImages/" + model.roomID + "2.jpg";
                SqlCommand cmd_add_images = new SqlCommand("insert roomimages values (@roomid,@image1,@image2)", con);
                cmd_add_images.Parameters.AddWithValue("@roomid", model.roomID);
                cmd_add_images.Parameters.AddWithValue("@image1", model.roomimage1);
                cmd_add_images.Parameters.AddWithValue("@image2", model.roomimage2);
                cmd_add_images.Transaction = tran;
               int count1=Convert.ToInt32(cmd_add_images.ExecuteNonQuery());
               if (count1 > 0)
               {
                   tran.Commit();
                   con.Close();
                   return true;
               }
               else {
                   tran.Rollback();
                   con.Close();
                   return false;
               
               }
                
                
               

            }
            public List<RoomModel> getRoomsonpgid(int pgid)
            {
                con.Open();
                SqlCommand cmd_get_rooms = new SqlCommand("select roomstable.roomID,roomstable.pgid,roomstable.roomname,roomstable.roomavailability,roomstable.roomamenities,roomstable.roomoccupancy,roomImages.roomimage1,roomImages.roomimage2,roomstable.roomrent from roomstable join roomImages on roomstable.roomID=roomImages.roomid where roomstable.pgid=@pgid", con);

                cmd_get_rooms.Parameters.AddWithValue("@pgid", pgid);
                SqlDataReader dr_room = cmd_get_rooms.ExecuteReader();
                List<RoomModel> list = new List<RoomModel>();
                while (dr_room.Read())
                {
                    RoomModel obj = new RoomModel();
                    obj.roomID = dr_room.GetInt32(0);
                    obj.pgID = dr_room.GetInt32(1);
                    obj.roomname = dr_room.GetString(2);
                    obj.roomavailability = dr_room.GetString(3);
                    obj.roomamenities = dr_room.GetString(4);
                    obj.roomoccupancy = dr_room.GetInt32(5);
                    obj.roomimage1 = dr_room.GetString(6);
                    obj.roomimage2 = dr_room.GetString(7);
                    obj.roomrent = dr_room.GetInt32(8);
                    list.Add(obj);
                 
                     }
               
                return list;
            }


 
        }
  
    }
