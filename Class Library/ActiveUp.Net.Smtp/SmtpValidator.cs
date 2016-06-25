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

//using System.Management;
using System;
using System.Web;

namespace ActiveUp.Net.Mail
{
    /// <summary>
    /// 
    /// </summary>
#if !PocketPC
    [Serializable]
#endif
    public class SmtpValidator : Validator
    {
        /// <summary>
        /// Validates syntax and existence of the given address.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool Validate(string address)
        {
            if (!ValidateSyntax(address))
                return false;
            else
            {
                try
                {
                    string domain = address.Split('@')[1];
                    bool result;
                    SmtpClient smtp = new SmtpClient();
                    smtp.SendTimeout = 0;
                    smtp.ReceiveTimeout = 0;
                    MxRecordCollection mxRecords = new MxRecordCollection();
                    try
                    {
                        mxRecords = GetMxRecords(domain);
                    }
                    catch
                    {
                        new Exception("Can't connect to DNS server.");
                    }
                    //Console.WriteLine(mxRecords.GetPrefered().Exchange);
                    if (mxRecords.Count > 0)
                        smtp.Connect(mxRecords.GetPrefered().Exchange);
                    else
                        return false;
                    try
                    {
                        smtp.Ehlo(System.Net.Dns.GetHostName());
                    }
                    catch
                    {
                        smtp.Helo(System.Net.Dns.GetHostName());
                    }
                    if (smtp.Verify(address))
                        result = true;
                    else
                    {
                        try
                        {
                            //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                            //smtp.MailFrom("postmaster@evolution-internet.com");
                            smtp.MailFrom("postmaster@" + domain);
                            smtp.RcptTo(address);
                            result = true;
                        }
                        catch (Exception ex)
                        {
#if (DEBUG)
                            Console.WriteLine(ex.ToString());
#endif
#if !PocketPC
                            HttpContext.Current.Trace.Write("ActiveMail", ex.ToString());
#endif
                            result = false;
                        }
                    }
                    smtp.Disconnect();
                    return result;
                }
                catch
                {
                    return false;
                }
            }
        }

        private delegate bool DelegateValidate(string address);
        private static DelegateValidate _delegateValidate;

        public static IAsyncResult BeginValidate(string address, AsyncCallback callback)
        {
            _delegateValidate = Validate;
            return _delegateValidate.BeginInvoke(address, callback, _delegateValidate);
        }


        /// <summary>
        /// Validates syntax and existence of the given address.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <param name="dnsServerHost">Name Server to be used for MX records search.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool Validate(string address, string dnsServerHost)
        {
            ServerCollection servers = new ServerCollection();
            servers.Add(dnsServerHost, 53);
            return Validate(address, servers);
        }

        private delegate bool DelegateValidateString(string address, string dnsServerHost);
        private static DelegateValidateString _delegateValidateString;

        public static IAsyncResult BeginValidate(string address, string dnsServerHost, AsyncCallback callback)
        {
            _delegateValidateString = Validate;
            return _delegateValidateString.BeginInvoke(address, dnsServerHost, callback, _delegateValidateString);
        }

        /// <summary>
        /// Validates syntax and existence of the given address.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <param name="dnsServers">Name Servers to be used for MX records search.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool Validate(string address, ServerCollection dnsServers)
        {
            if (!ValidateSyntax(address))
                return false;
            else
            {
                string domain = address.Split('@')[1];
                bool result;
                SmtpClient smtp = new SmtpClient();
                smtp.SendTimeout = 15;
                smtp.ReceiveTimeout = 15;

                MxRecordCollection mxRecords = new MxRecordCollection();
                try
                {
#if !PocketPC
                    mxRecords = GetMxRecords(domain, dnsServers);
#else
                    mxRecords = ActiveUp.Net.Mail.Validator.GetMxRecords(domain);
#endif
                }
                catch
                {
                    new Exception("Can't connect to DNS server.");
                }
                smtp.Connect(mxRecords.GetPrefered().Exchange);
                try
                {
                    smtp.Ehlo(System.Net.Dns.GetHostName());
                }
                catch
                {
                    smtp.Helo(System.Net.Dns.GetHostName());
                }
                if (smtp.Verify(address)) result = true;
                else
                {
                    try
                    {
                        //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                        //smtp.MailFrom("postmaster@evolution-internet.com");
                        smtp.MailFrom("postmaster@" + domain);
                        smtp.RcptTo(address);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                }
                smtp.Disconnect();
                return result;
            }
        }

        private delegate bool DelegateValidateStringServers(string address, ServerCollection servers);
        private static DelegateValidateStringServers _delegateValidateStringServers;

        public static IAsyncResult BeginValidate(string address, ServerCollection servers, AsyncCallback callback)
        {
            _delegateValidateStringServers = Validate;
            return _delegateValidateStringServers.BeginInvoke(address, servers, callback, _delegateValidateStringServers);
        }

        /// <summary>
        /// Validates syntax and existence of the given address.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool Validate(Address address)
        {
            return Validate(address.Email);
        }

        private delegate bool DelegateValidateAddress(Address address);
        private static DelegateValidateAddress _delegateValidateAddress;

        public static IAsyncResult BeginValidate(Address address, AsyncCallback callback)
        {
            _delegateValidateAddress = Validate;
            return _delegateValidateAddress.BeginInvoke(address, callback, _delegateValidateAddress);
        }

        /// <summary>
        /// Validates syntax and existence of the given address.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <param name="dnsServers">Name Servers to be used for MX records search.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool Validate(Address address, ServerCollection dnsServers)
        {
            return Validate(address.Email, dnsServers);
        }

        private delegate bool DelegateValidateAddressServers(Address address, ServerCollection dnsServers);
        private static DelegateValidateAddressServers _delegateValidateAddressServers;

        public static IAsyncResult BeginValidate(Address address, ServerCollection dnsServers, AsyncCallback callback)
        {
            _delegateValidateAddressServers = Validate;
            return _delegateValidateAddressServers.BeginInvoke(address, dnsServers, callback, _delegateValidateAddressServers);
        }

        /// <summary>
        /// Validates syntax and existence of the given address.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        /// <param name="dnsServerHost">Name Server to be used for MX records search.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool Validate(Address address, string dnsServerHost)
        {
            ServerCollection servers = new ServerCollection();
            servers.Add(dnsServerHost, 53);
            return Validate(address.Email, servers);
        }

        private delegate bool DelegateValidateAddressString(Address address, string dnsServerHost);
        private static DelegateValidateAddressString _delegateValidateAddressString;

        public static IAsyncResult BeginValidate(Address address, string dnsServerHost, AsyncCallback callback)
        {
            _delegateValidateAddressString = Validate;
            return _delegateValidateAddressString.BeginInvoke(address, dnsServerHost, callback, _delegateValidateAddressString);
        }

        public bool EndValidate(IAsyncResult result)
        {
            return (bool)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Validates syntax and existence of the given addresses and returns a collection of invalid or inexistent addresses.
        /// </summary>
        /// <param name="addresses">The addresses to be examined.</param>
        /// <returns>A collection containing the invalid addresses.</returns>
        public static AddressCollection GetInvalidAddresses(AddressCollection addresses)
        {
            AddressCollection invalids = new AddressCollection();
            AddressCollection valids = new AddressCollection();
            System.Collections.Specialized.HybridDictionary ads = new System.Collections.Specialized.HybridDictionary();
            for (int i = 0; i < addresses.Count; i++)
                if (!ValidateSyntax(addresses[i].Email))
                    invalids.Add(addresses[i]);
                else
                    valids.Add(addresses[i]);
#if !PocketPC
            Array domains = Array.CreateInstance(typeof(string), new int[] { valids.Count }, new int[] { 0 });
            Array adds = Array.CreateInstance(typeof(Address), new int[] { valids.Count }, new int[] { 0 });
#else
            System.Array domains = System.Array.CreateInstance(typeof(string), new int[] { valids.Count });
            System.Array adds = System.Array.CreateInstance(typeof(ActiveUp.Net.Mail.Address), new int[] { valids.Count });
#endif

            for (int i = 0; i < valids.Count; i++)
            {
                domains.SetValue(valids[i].Email.Split('@')[1], i);
                adds.SetValue(valids[i], i);
            }
            Array.Sort(domains, adds, null);
            string currentDomain = "";
            string address = "";
            SmtpClient smtp = new SmtpClient();
            bool isConnected = false;
            for (int i = 0; i < adds.Length; i++)
            {
                address = ((Address)adds.GetValue(i)).Email;
                if (((string)domains.GetValue(i)) == currentDomain)
                {
                    if (!smtp.Verify(address))
                    {
                        try
                        {
                            //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                            smtp.RcptTo(address);
                        }
                        catch
                        {
                            invalids.Add((Address)adds.GetValue(i));
                        }
                    }
                }
                else
                {
                    currentDomain = (string)domains.GetValue(i);
                    try
                    {
                        if (isConnected == true)
                        {
                            isConnected = false;
                            smtp.Disconnect();
                            smtp = new SmtpClient();
                        }

                        smtp.Connect(GetMxRecords(currentDomain).GetPrefered().Exchange);
                        isConnected = true;
                        try
                        {
                            smtp.Ehlo(System.Net.Dns.GetHostName());
                        }
                        catch
                        {
                            smtp.Helo(System.Net.Dns.GetHostName());
                        }
                        if (!smtp.Verify(address))
                        {
                            try
                            {
                                //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                                //smtp.MailFrom("postmaster@evolution-internet.com");
                                smtp.MailFrom("postmaster@" + currentDomain);
                                smtp.RcptTo(address);
                            }
                            catch
                            {
                                invalids.Add((Address)adds.GetValue(i));
                            }
                        }
                    }
                    catch
                    {
                        invalids.Add((Address)adds.GetValue(i));
                    }
                }
            }
            if (isConnected == true)
                smtp.Disconnect();
            return invalids;
        }

        private delegate AddressCollection DelegateGetInvalidAddresses(AddressCollection addresses);
        private static DelegateGetInvalidAddresses _delegateGetInvalidAddresses;

        public static IAsyncResult BeginGetInvalidAddresses(AddressCollection addresses, AsyncCallback callback)
        {
            _delegateGetInvalidAddresses = GetInvalidAddresses;
            return _delegateGetInvalidAddresses.BeginInvoke(addresses, callback, _delegateGetInvalidAddresses);
        }

        /// <summary>
        /// Validates syntax and existence of the given address and returns valid addresses.
        /// </summary>
        /// <param name="addresses">The collection to be filtered.</param>
        /// <returns>A collection containing the valid addresses.</returns>
        public static AddressCollection Filter(AddressCollection addresses)
        {
            AddressCollection valids = new AddressCollection();
            AddressCollection valids1 = new AddressCollection();
            System.Collections.Specialized.HybridDictionary ads = new System.Collections.Specialized.HybridDictionary();
            for (int i = 0; i < addresses.Count; i++)
                if (ValidateSyntax(addresses[i].Email))
                    valids.Add(addresses[i]);
#if !PocketPC
            Array domains = Array.CreateInstance(typeof(string), new int[] { valids.Count }, new int[] { 0 });
            Array adds = Array.CreateInstance(typeof(Address), new int[] { valids.Count }, new int[] { 0 });
#else
            System.Array domains = System.Array.CreateInstance(typeof(string), new int[] { valids.Count });
            System.Array adds = System.Array.CreateInstance(typeof(ActiveUp.Net.Mail.Address), new int[] { valids.Count });
#endif
            for (int i = 0; i < valids.Count; i++)
            {
                domains.SetValue(valids[i].Email.Split('@')[1], i);
                adds.SetValue(valids[i], i);
            }
            Array.Sort(domains, adds, null);
            string currentDomain = "";
            string address = "";
            SmtpClient smtp = new SmtpClient();
            bool isConnected = false;
            for (int i = 0; i < adds.Length; i++)
            {
                address = ((Address)adds.GetValue(i)).Email;
                if (((string)domains.GetValue(i)) == currentDomain)
                {
                    if (!smtp.Verify(address))
                    {
                        try
                        {
                            //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                            //smtp.MailFrom("postmaster@"+currentDomain);
                            smtp.RcptTo(address);
                            valids1.Add((Address)adds.GetValue(i));
                        }
                        catch
                        {

                        }
                    }
                    else valids1.Add((Address)adds.GetValue(i));
                }
                else
                {
                    currentDomain = (string)domains.GetValue(i);
                    try
                    {
                        if (isConnected == true)
                        {
                            isConnected = false;
                            smtp.Disconnect();
                            smtp = new SmtpClient();
                        }

                        smtp.Connect(GetMxRecords(currentDomain).GetPrefered().Exchange);
                        isConnected = true;
                        try
                        {
                            smtp.Ehlo(System.Net.Dns.GetHostName());
                        }
                        catch
                        {
                            smtp.Helo(System.Net.Dns.GetHostName());
                        }
                        if (!smtp.Verify(address))
                        {
                            try
                            {

                                //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                                //smtp.MailFrom("postmaster@evolution-internet.com");
                                smtp.MailFrom("postmaster@" + currentDomain);
                                smtp.RcptTo(address);
                                valids1.Add((Address)adds.GetValue(i));
                            }
                            catch
                            {

                            }
                        }
                        else valids1.Add((Address)adds.GetValue(i));
                    }
                    catch
                    {

                    }
                }
            }
            if (isConnected == true)
                smtp.Disconnect();
            return valids1;
        }

        private delegate AddressCollection DelegateFilter(AddressCollection addresses);
        private static DelegateFilter _delegateFilter;

        public static IAsyncResult BeginFilter(AddressCollection addresses, AsyncCallback callback)
        {
            _delegateFilter = Filter;
            return _delegateFilter.BeginInvoke(addresses, callback, _delegateFilter);
        }

        /// <summary>
        /// Validates syntax and existence of the given addresses and returns a collection of invalid or inexistent addresses.
        /// </summary>
        /// <param name="addresses">The addresses to be examined.</param>
        /// <param name="dnsServers">Name Servers to be used for MX records search.</param>
        /// <returns>A collection containing the invalid addresses.</returns>
        public static AddressCollection GetInvalidAddresses(AddressCollection addresses, ServerCollection dnsServers)
        {
            AddressCollection invalids = new AddressCollection();
            AddressCollection valids = new AddressCollection();
            System.Collections.Specialized.HybridDictionary ads = new System.Collections.Specialized.HybridDictionary();
            for (int i = 0; i < addresses.Count; i++)
                if (!ValidateSyntax(addresses[i].Email))
                    invalids.Add(addresses[i]);
                else
                    valids.Add(addresses[i]);
#if !PocketPC
            Array domains = Array.CreateInstance(typeof(string), new int[] { valids.Count }, new int[] { 0 });
            Array adds = Array.CreateInstance(typeof(Address), new int[] { valids.Count }, new int[] { 0 });
#else
            System.Array domains = System.Array.CreateInstance(typeof(string), new int[] { valids.Count });
            System.Array adds = System.Array.CreateInstance(typeof(ActiveUp.Net.Mail.Address), new int[] { valids.Count });
#endif
            for (int i = 0; i < valids.Count; i++)
            {
                domains.SetValue(valids[i].Email.Split('@')[1], i);
                adds.SetValue(valids[i], i);
            }
            Array.Sort(domains, adds, null);
            string currentDomain = "";
            string address = "";
            SmtpClient smtp = new SmtpClient();
            bool isConnected = false;
            for (int i = 0; i < adds.Length; i++)
            {
                address = ((Address)adds.GetValue(i)).Email;
                if (((string)domains.GetValue(i)) == currentDomain)
                {
                    if (!smtp.Verify(address))
                    {
                        try
                        {
                            //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                            //smtp.MailFrom("postmaster@"+currentDomain);
                            smtp.RcptTo(address);
                        }
                        catch
                        {
                            invalids.Add((Address)adds.GetValue(i));
                        }
                    }
                }
                else
                {
                    currentDomain = (string)domains.GetValue(i);
                    try
                    {
                        if (isConnected == true)
                        {
                            isConnected = false;
                            smtp.Disconnect();
                            smtp = new SmtpClient();
                        }

                        smtp.Connect(GetMxRecords(currentDomain, dnsServers).GetPrefered().Exchange);
                        isConnected = true;
                        try
                        {
                            smtp.Ehlo(System.Net.Dns.GetHostName());
                        }
                        catch
                        {
                            smtp.Helo(System.Net.Dns.GetHostName());
                        }
                        if (!smtp.Verify(address))
                        {
                            try
                            {
                                //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                                //smtp.MailFrom("postmaster@evolution-internet.com");
                                smtp.MailFrom("postmaster@" + currentDomain);
                                smtp.RcptTo(address);
                            }
                            catch
                            {
                                invalids.Add((Address)adds.GetValue(i));
                            }
                        }
                    }
                    catch
                    {
                        invalids.Add((Address)adds.GetValue(i));
                    }
                }
            }
            if (isConnected == true)
                smtp.Disconnect();
            return invalids;
        }

        private delegate AddressCollection DelegateGetInvalidAddressesServers(AddressCollection addresses, ServerCollection dnsServers);
        private static DelegateGetInvalidAddressesServers _delegateGetInvalidAddressesServers;

        public static IAsyncResult BeginGetInvalidAddresses(AddressCollection addresses, ServerCollection dnsServers, AsyncCallback callback)
        {
            _delegateGetInvalidAddressesServers = GetInvalidAddresses;
            return _delegateGetInvalidAddressesServers.BeginInvoke(addresses, dnsServers, callback, _delegateGetInvalidAddressesServers);
        }

        public AddressCollection EndGetInvalidAddresses(IAsyncResult result)
        {
            return (AddressCollection)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }

        /// <summary>
        /// Validates syntax and existence of the given address and returns valid addresses.
        /// </summary>
        /// <param name="addresses">The collection to be filtered.</param>
        /// <param name="dnsServers">Name Servers to be used for MX records search.</param>
        /// <returns>A collection containing the valid addresses.</returns>
        public static AddressCollection Filter(AddressCollection addresses, ServerCollection dnsServers)
        {
            AddressCollection valids = new AddressCollection();
            AddressCollection valids1 = new AddressCollection();
            System.Collections.Specialized.HybridDictionary ads = new System.Collections.Specialized.HybridDictionary();
            for (int i = 0; i < addresses.Count; i++)
                if (ValidateSyntax(addresses[i].Email)) valids.Add(addresses[i]);
#if !PocketPC
            Array domains = Array.CreateInstance(typeof(string), new int[] { valids.Count }, new int[] { 0 });
            Array adds = Array.CreateInstance(typeof(Address), new int[] { valids.Count }, new int[] { 0 });
#else
            System.Array domains = System.Array.CreateInstance(typeof(string), new int[] { valids.Count });
            System.Array adds = System.Array.CreateInstance(typeof(ActiveUp.Net.Mail.Address), new int[] { valids.Count });
#endif
            for (int i = 0; i < valids.Count; i++)
            {
                domains.SetValue(valids[i].Email.Split('@')[1], i);
                adds.SetValue(valids[i], i);
            }
            Array.Sort(domains, adds, null);
            string currentDomain = "";
            string address = "";
            SmtpClient smtp = new SmtpClient();
            bool isConnected = false;
            for (int i = 0; i < adds.Length; i++)
            {
                address = ((Address)adds.GetValue(i)).Email;
                if (((string)domains.GetValue(i)) == currentDomain)
                {
                    if (!smtp.Verify(address))
                    {
                        try
                        {
                            //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                            //smtp.MailFrom("postmaster@"+currentDomain);
                            smtp.RcptTo(address);
                            valids1.Add((Address)adds.GetValue(i));
                        }
                        catch
                        {

                        }
                    }
                    else valids1.Add((Address)adds.GetValue(i));
                }
                else
                {
                    currentDomain = (string)domains.GetValue(i);
                    try
                    {
                        if (isConnected == true)
                        {
                            isConnected = false;
                            smtp.Disconnect();
                            smtp = new SmtpClient();
                        }

                        smtp.Connect(GetMxRecords(currentDomain, dnsServers).GetPrefered().Exchange);
                        isConnected = true;
                        try
                        {
                            smtp.Ehlo(System.Net.Dns.GetHostName());
                        }
                        catch
                        {
                            smtp.Helo(System.Net.Dns.GetHostName());
                        }
                        if (!smtp.Verify(address))
                        {
                            try
                            {
                                //smtp.MailFrom("postmaster@"+System.Net.Dns.GetHostName());
                                //smtp.MailFrom("postmaster@evolution-internet.com");
                                smtp.MailFrom("postmaster@" + currentDomain);
                                smtp.RcptTo(address);
                                valids1.Add((Address)adds.GetValue(i));
                            }
                            catch
                            {

                            }
                        }
                        else valids1.Add((Address)adds.GetValue(i));
                    }
                    catch
                    {

                    }
                }
            }
            if (isConnected == true)
                smtp.Disconnect();
            return valids1;
        }

        private delegate AddressCollection DelegateFilterServers(AddressCollection addresses, ServerCollection dnsServers);
        private static DelegateFilterServers _delegateFilterServers;

        public static IAsyncResult BeginFilter(AddressCollection addresses, ServerCollection dnsServers, AsyncCallback callback)
        {
            _delegateFilterServers = Filter;
            return _delegateFilterServers.BeginInvoke(addresses, dnsServers, callback, _delegateFilterServers);

        }

        public AddressCollection EndFilter(IAsyncResult result)
        {
            return (AddressCollection)result.AsyncState.GetType().GetMethod("EndInvoke").Invoke(result.AsyncState, new object[] { result });
        }
    }
}
