﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=LAPTOP-1I5G221A;Initial Catalog=WCFReservasi;Integrated Security=True";
        SqlConnection connection;
        SqlCommand comm;

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "SELECT ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi";
                connection = new SqlConnection(constring);
                comm = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "select ID_reservasi, Nama_customer, No_telpon, Jumlah_pemesanan, Nama_Lokasi FROM dbo.Pemesanan p join dbo. p.ID_lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring);
                comm = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelepon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string n = "gagal";
            try
            {
                string sql = "INSERT INTO dbo.Pemesanan VALUES('" + IDPemesanan + "','" + NamaCustomer + "','" + NoTelpon + "'," + JumlahPemesanan + ",'" + IDLokasi + "')";
                connection = new SqlConnection(constring);
                comm = new SqlCommand(sql, connection);
                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();

                string sql2 = "UPDATE dbo.Lokasi set Kuota = Kuota - " + JumlahPemesanan + " WHERE ID_lokasi = '" + IDLokasi + "' ";
                connection = new SqlConnection(constring);
                comm = new SqlCommand(sql2, connection);
                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();

                n = "Berhasil";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return n;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {
            string n = "gagal";
            try
            {
                string sql = "UPDATE dbo.Pemesanan SET Nama_customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "' WHERE ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring);
                comm = new SqlCommand(sql, connection);
                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();

                n = "Berhasil";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return n;
        }

        public string deletePemesanan(string IDPemesanan)
        {
            string n = "gagal";
            try
            {
                string sql = "DELETE FROM dbo.Pemesanan WHERE ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring);
                comm = new SqlCommand(sql, connection);
                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();

                n = "Berhasil";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return n;
        }
    }
}
