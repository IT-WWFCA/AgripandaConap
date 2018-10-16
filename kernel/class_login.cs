/**
 * WWF Honduras
 * AgriPanda v1.1.0
 * Login Encryption Handler
 * Last Updated: $Date: 2012-03-13 11:16:08 -0600 (Thu, 01 Jul 2010) $
 *
 * @author 		$Author: Arnold Lara $
 * @copyright	(c) 2009 - 2012 WWF Honduras.
 * @license		http://www.wwf-mar.org/aplicense/
 * @package		AgriPanda
 * @subpackage	AP.Login-Encryption
 * @link		http://www.wwf-mar.org
 * @since		1.1.0
 * @version		$Revision: 0005 $
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AgriPanda.kernel
{
    public class class_login
    {
        private string _password;
        private int _salt;

        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// Username and Password functions
        ///-----------------------------------------
        ///--------------------------------------------------------------------
        /// <summary>
        /// Test for wolfrayet row.
        /// </summary>	
        public void wolfrayet_check_for_member_by_email(string email)
        {
            string[,] simple_query_arr = { { "select", "UserID" }, { "from", "users" }, { "where", "UserEmail=" + email } };
            //string test = DB.simple_exec_query( simple_query_arr );

            /*if ( test["UserID"] != "") 
            {
                return TRUE;
            }
            else
            {
                return FALSE;
            }*/

        }

        /// <summary>
        /// Create a new random password.
        /// </summary>
        public static string CreateRandomPassword(int PasswordLength)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        /// <summary>
        /// Generate new random salt key.
        /// </summary>
        public static int CreateRandomSalt()
        {
            Byte[] _saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
                (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }

        /// <summary>
        /// Compute the salted hash.
        /// </summary>
        public string ComputeSaltedHash(string strPassword, int nSalt)
        {
            _password = strPassword;
            _salt = nSalt;

            // Create Byte array of password string
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] _secretBytes = encoder.GetBytes(_password);

            // Create a new salt
            Byte[] _saltBytes = new Byte[4];
            _saltBytes[0] = (byte)(_salt >> 24);
            _saltBytes[1] = (byte)(_salt >> 16);
            _saltBytes[2] = (byte)(_salt >> 8);
            _saltBytes[3] = (byte)(_salt);

            // append the two arrays
            Byte[] toHash = new Byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);

            SHA512 sha512 = SHA512.Create();
            Byte[] computedHash = sha512.ComputeHash(toHash);

            return encoder.GetString(computedHash);
        }
    }
}