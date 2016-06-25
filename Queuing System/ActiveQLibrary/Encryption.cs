// Copyright 2001-2010 - Active Up SPRLU (http://www.agilecomponents.com)
//
// This file is part of MailSystem.NET.
// MailSystem.NET is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// MailSystem.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ActiveQLibrary
{
	/// <summary>
	/// Description résumée de Encryption.
	/// </summary>
	public class Encryption
	{
		// NB that the keys sit in the code, so this is NOT bullet-proof!!!
		public static string Encrypt(string pToEncrypt) 
		{
			return Encrypt(pToEncrypt, "4Jkw9N3f");
		}
		public static string Encrypt(string pToEncrypt, string sKey) 
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();

			//Put the string into a byte array
			byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);

			//Create the crypto objects, with the key, as passed in
			des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
			des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(),
				CryptoStreamMode.Write);
			//Write the byte array into the crypto stream
			//(It will end up in the memory stream)
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();

			//Get the data back from the memory stream, and into a string
			StringBuilder ret = new StringBuilder();
			foreach(byte b in ms.ToArray())
			{
				//Format as hex
				ret.AppendFormat("{0:X2}", b);
			}
			return ret.ToString();
		}

		public static string Decrypt(string pToDecrypt) 
		{
			return Decrypt(pToDecrypt, "4Jkw9N3f");
		}

		public static string Decrypt(string pToDecrypt, string sKey) 
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();

			//Put the input string into the byte array
			byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
			for(int x = 0; x < pToDecrypt.Length / 2; x++)
			{
				int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
				inputByteArray[x] = (byte)i;
			}

			//Create the crypto objects
			des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
			des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(),
				CryptoStreamMode.Write);
			//Flush the data through the crypto stream into the memory stream
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();

			//Get the decrypted data back from the memory stream
			StringBuilder ret = new StringBuilder();
			foreach(byte b in ms.ToArray())
			{
				ret.Append((char)b);
			}
			return ret.ToString();
		}
	}
}
