using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MyApp.Entity;

namespace Spotify.Models
{
    public class MyAppDB
    {
        private static string config = ConfigurationManager.ConnectionStrings["CON_NAME"].ConnectionString;
        public DataTable dtOnly { get; set; }

        public int addSong(SongsEntity sng)
        {
            try
            {

                SqlConnection con = new SqlConnection(config);
                SqlCommand cmd = new SqlCommand("SP_SONG", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", sng.songName);
                cmd.Parameters.AddWithValue("@RLSDATE", sng.rlsDate);
                cmd.Parameters.AddWithValue("@ARTWORK", sng.image);
                cmd.Parameters.AddWithValue("@ARTIST", sng.artists);
                cmd.Parameters.AddWithValue("@TASK", "CREATE");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int addArtist(ArtistsEntity art)
        {
            try
            {

                SqlConnection con = new SqlConnection(config);
                SqlCommand cmd = new SqlCommand("SP_ARTIST", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", art.artistName);
                cmd.Parameters.AddWithValue("@DOB", art.dob);
                cmd.Parameters.AddWithValue("@BIO", art.bio);               
                cmd.Parameters.AddWithValue("@TASK", "CREATE");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable selectArtist()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(config);
                SqlCommand cmd = new SqlCommand("SP_ARTIST", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@TASK", "SELECT");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable selectSongs()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(config);
                SqlCommand cmd = new SqlCommand("SP_SONG", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TASK", "SELECT");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

    }
}