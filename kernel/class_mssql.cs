/**
 * WWF Honduras
 * AgriPanda v2.0.0
 * MS SQL Handler
 * Last Updated: $Date: 2016-03-03 13:33:08 -0600 (Tue, 22 Jun 2010) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2016 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.MSSQL
 * @link		http://www.wwf-mar.org
 * @since		2.0.0
 * @version		$Revision: 0006 $
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AgriPanda.kernel
{
    public class class_mssql
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["AgriPanda"].ConnectionString);

        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// QUICK SQL FUNCTIONS
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /*========================================================================*/
        /// Create an array from a multidimensional array returning a formatted
        /// string ready to use in an UPDATE query, saves having to manually format
        /// the FIELD='val', FIELD='val', FIELD='val'
        /*========================================================================*/
        /// <summary>
        /// DB Update Function.
        /// </summary>
        public string do_update(string table, string[,] arr, string where)
        {
            string query = string.Empty;
            string set = string.Empty;
            string mesg = string.Empty;
            for (int i = 0; i <= arr.GetUpperBound(0); i++)
            {
                string s1 = arr[i, 0]; // First column...
                object s2 = arr[i, 1]; // Second column...
                bool isnum = class_global.IsNumeric(s2);
                if (isnum)
                {
                    s2 = Regex.Replace(s2.ToString(), ",", "");
                    set += s1 + "=" + s2 + ",";
                }
                else
                {
                    bool isdate = class_global.CheckDate(s2.ToString());
                    if (isdate)
                        set += s1 + "=CONVERT(datetime, '" + s2 + "', 20),";
                    else
                        set += s1 + "='" + s2 + "',";
                }
                    
            }
            set = Regex.Replace(set, ",$", "");
            query = simple_update(table, set, where, "");
            try { con.Open(); SqlCommand cmd = new SqlCommand(query, con); cmd.ExecuteNonQuery(); }
            catch (SqlException ex) { mesg = ex.Message; }
            finally { con.Close(); }
            return mesg;
        }

        /*========================================================================*/
        // Create an array from a multidimensional array returning formatted
        // strings ready to use in an INSERT query, saves having to manually format
        // the (INSERT INTO table) ('field', 'field', 'field') VALUES ('val', 'val')
        /*========================================================================*/
        /// <summary>
        /// DB Insert Function.
        /// </summary>
        public string do_insert(string table, string[,] arr)
        {
            string query = string.Empty;
            string fields = string.Empty;
            string values = string.Empty;
            string mesg = string.Empty;
            for (int i = 0; i <= arr.GetUpperBound(0); i++)
            {
                string s1 = arr[i, 0]; // First column...
                object s2 = arr[i, 1]; // Second column...
                fields += s1 + ",";
                bool isnum = class_global.IsNumeric(s2);
                if (isnum)
                {
                    s2 = Regex.Replace(s2.ToString(), ",", "");
                    values += s2 + ",";
                }
                else
                {
                    bool isdate = class_global.CheckDate(s2.ToString());
                    if (isdate)
                        values += "CONVERT(datetime, '" + s2 + "', 20),";
                    else
                        values += "'" + s2 + "',";
                }
            }
            fields = Regex.Replace(fields, ",$", "");
            values = Regex.Replace(values, ",$", "");
            query = simple_insert(table, fields, values);
            try { con.Open(); SqlCommand cmd = new SqlCommand(query, con); cmd.ExecuteNonQuery(); }
            catch (SqlException ex) { mesg = ex.Message; }
            finally { con.Close(); }
            return mesg;
        }

        /// <summary>
        /// DB Insert Function with ID Output.
        /// </summary>
        public string do_insert_iden(string table, string[,] arr)
        {
            string query = string.Empty;
            string fields = string.Empty;
            string values = string.Empty;
            string mesg = string.Empty;
            for (int i = 0; i <= arr.GetUpperBound(0); i++)
            {
                string s1 = arr[i, 0]; // First column...
                object s2 = arr[i, 1]; // Second column...
                fields += s1 + ",";
                bool isnum = class_global.IsNumeric(s2);
                if (isnum)
                {
                    s2 = Regex.Replace(s2.ToString(), ",", "");
                    values += s2 + ",";
                }
                else
                {
                    bool isdate = class_global.CheckDate(s2.ToString());
                    if (isdate)
                        values += "CONVERT(datetime, '" + s2 + "', 20),";
                    else
                        values += "'" + s2 + "',";
                }
            }
            fields = Regex.Replace(fields, ",$", "");
            values = Regex.Replace(values, ",$", "");
            query = simple_insert_iden(table, fields, values);
            try { con.Open(); SqlCommand cmd = new SqlCommand(query, con); cmd.ExecuteNonQuery(); }
            catch (SqlException ex) { mesg = ex.Message; }
            finally { con.Close(); }
            return mesg;
        }

        /// <summary>
        /// DB Delete Function.
        /// </summary>
        public string do_delete(string table, string where)
        {
            string query = string.Empty;
            string mesg = string.Empty;
            query = simple_delete(table, where);
            try { con.Open(); SqlCommand cmd = new SqlCommand(query, con); cmd.ExecuteNonQuery(); }
            catch (SqlException ex) { mesg = ex.Message; }
            finally { con.Close(); }
            return mesg;
        }

        /// <summary>
        /// Simple update.
        /// </summary>
        public string simple_update(string tbl, string set, string where, string low_pro)
        {
            string cur_query = string.Empty;
            if (low_pro != "") { low_pro = " LOW_PRIORITY "; }
            cur_query += "UPDATE " + low_pro + tbl + " SET " + set;
            if (where != "") { cur_query += " WHERE " + where; }
            return cur_query;
        }

        /// <summary>
        /// Simple insert.
        /// </summary>
        public string simple_insert(string tbl, string fields, string values)
        {
            string cur_query = string.Empty;
            cur_query += "INSERT INTO " + tbl + " (" + fields + ") VALUES (" + values + ")";
            return cur_query;
        }

        /// <summary>
        /// Simple insert with ID Output.
        /// </summary>
        public string simple_insert_iden(string tbl, string fields, string values)
        {
            string cur_query = string.Empty;
            cur_query += "INSERT INTO " + tbl + " (" + fields + ") VALUES (" + values + ")";
            return cur_query;
        }

        /// <summary>
        /// Simple delete.
        /// </summary>
        public string simple_delete(string tbl, string where)
        {
            string cur_query = string.Empty;
            cur_query += "DELETE FROM " + tbl;
            if (where != "") { cur_query += " WHERE " + where; }
            return cur_query;
        }
    }
}