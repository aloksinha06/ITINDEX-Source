using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ITINDEXAU.Class
{
    public class CodeClass : Controller
    {
        //
        // GET: /CodeClass1/

        SqlConnection con = new SqlConnection(new Connection().connection());
        public int InsertUpdateDelete(string query)
        {
            SqlCommand com = new SqlCommand(query,con);
            try
            {
                com.ExecuteNonQuery();
                return 1;
            }
            catch
            {
                return 0;
            }
           
        }
        public DataTable fillsinglecombo(string query)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = query;
            SqlDataAdapter dad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            using (dad)
            {
                try
                {
                    dad.Fill(dt);
                }
                catch
                {
                }

            }
            return dt;
        }       
        public DataTable FillGridView(string querystring)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = querystring;
            SqlDataAdapter dad = new SqlDataAdapter(com);
            com.CommandTimeout = 0;
            DataTable dt = new DataTable();
            using (dad)
            {
                dad.Fill(dt);

            }
            if (dt.Rows.Count != 0)
            {
                return dt;
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                return dt;
            }

        }
        public DataTable  FillDetailsView(string querystring)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = querystring;
            SqlDataAdapter dad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            using (dad)
            {
                dad.Fill(dt);

            }
            if (dt.Rows.Count != 0)
            {
                return  dt;
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                return dt;
            }
        }
        public DataTable fillsinglegrid(string query)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = query;
            SqlDataAdapter dad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            using (dad)
            {
                dad.Fill(dt);

            }
            return dt;
        }
        public Decimal ScalerReturnDecimal(string QueryString)
        {
            Decimal Value;

            SqlCommand cmd = new SqlCommand(QueryString, con);
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            object ob;
            try
            {
                ob = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                ob = 0;
            }


            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ob == DBNull.Value)
            {
                Value = 0;
            }
            else
            {
                try
                {
                    Value = Decimal.Parse(ob.ToString());
                }
                catch (Exception ex)
                {
                    Value = 0;
                }
            }
            return (Value);


        }
        public int ScalerReturnInt(string QueryString)
        {
            int Value;

            SqlCommand cmd = new SqlCommand(QueryString, con);
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            object ob;
            try
            {
                ob = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                ob = 0;
            }


            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ob == DBNull.Value)
            {
                Value = 0;
            }
            else
            {
                try
                {
                    Value = int.Parse(ob.ToString());
                }
                catch (Exception ex)
                {
                    Value = 0;
                }
            }
            return (Value);


        }
        public string ScalerReturnString(string QueryString)
        {
            string Value;
            SqlCommand cmd = new SqlCommand(QueryString, con);
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            object ob = cmd.ExecuteScalar();
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            try
            {
                if (ob == DBNull.Value)
                {
                    Value = "";
                }
                else
                {
                    Value = ob.ToString();
                }
                return (Value);
            }
            catch (Exception e)
            {
                Value = "";
                return (Value);
            }

        }
        public Double ScalerReturnDouble(string QueryString)
        {
            Double Value;

            SqlCommand cmd = new SqlCommand(QueryString, con);
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            object ob;
            try
            {
                ob = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                ob = 0;
            }


            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ob == DBNull.Value)
            {
                Value = 0;
            }
            else
            {
                try
                {
                    Value = Double.Parse(ob.ToString());
                }
                catch (Exception ex)
                {
                    Value = 0;
                }
            }
            return (Value);


        }
        public DateTime ScalerReturnDateTime(string QueryString)
        {
            string date = "08/15/2047";
            DateTime Value = DateTime.Now;
            SqlCommand cmd = new SqlCommand(QueryString, con);
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            object ob = cmd.ExecuteScalar();
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ob == DBNull.Value)
            {
                //Value = DateTime.Parse(date.ToString());
            }
            else
            {
                Value = DateTime.Parse(ob.ToString());
            }
            return Value;


        }

    }
}
