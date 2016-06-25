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


namespace ActiveUp.Net.Security
{
#if !PocketPC
    public class SslHandShake
    {
        #region Constructors

        public SslHandShake(string hostName, System.Security.Authentication.SslProtocols sslProtocol, System.Net.Security.RemoteCertificateValidationCallback serverCallback, System.Net.Security.LocalCertificateSelectionCallback clientCallback, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, bool checkRevocation)
        {
            _hostName = hostName;
            _sslProtocol = sslProtocol;
            _serverCallback = serverCallback;
            _clientCallback = clientCallback;
            _clientCertificates = clientCertificates;
            _checkRevocation = checkRevocation;
        }
        public SslHandShake(string hostName, System.Security.Authentication.SslProtocols sslProtocol, System.Net.Security.RemoteCertificateValidationCallback serverCallback, System.Net.Security.LocalCertificateSelectionCallback clientCallback, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates) : this(hostName, sslProtocol, serverCallback, clientCallback, clientCertificates, false)
        {

        }
        public SslHandShake(string hostName, System.Security.Authentication.SslProtocols sslProtocol, System.Net.Security.RemoteCertificateValidationCallback serverCallback, System.Net.Security.LocalCertificateSelectionCallback clientCallback) : this(hostName, sslProtocol, serverCallback, clientCallback, null, false)
        {

        }
        public SslHandShake(string hostName, System.Security.Authentication.SslProtocols sslProtocol, System.Net.Security.RemoteCertificateValidationCallback serverCallback) : this(hostName, sslProtocol, serverCallback, null, null, false)
        {

        }
        public SslHandShake(string hostName, System.Security.Authentication.SslProtocols sslProtocol) : this(hostName, sslProtocol, null, null, null, false)
        {

        }
        public SslHandShake(string hostName) : this(hostName, System.Security.Authentication.SslProtocols.Default, null, null, null, false)
        {

        }

        #endregion

        #region Private fields

        private string _hostName;

        private System.Security.Authentication.SslProtocols _sslProtocol = System.Security.Authentication.SslProtocols.Default;
        private System.Net.Security.LocalCertificateSelectionCallback _clientCallback;
        private System.Net.Security.RemoteCertificateValidationCallback _serverCallback;
        private System.Security.Cryptography.X509Certificates.X509CertificateCollection _clientCertificates;
        private bool _checkRevocation = false;

        #endregion

        #region Properties

        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
            }
        }

        public System.Security.Authentication.SslProtocols SslProtocol
        {
            get
            {
                return _sslProtocol;
            }
            set
            {
                _sslProtocol = value;
            }
        }

        public System.Net.Security.LocalCertificateSelectionCallback ClientCertificateSelectionCallback
        {
            get
            {
                return _clientCallback;
            }
            set
            {
                _clientCallback = value;
            }
        }
        public System.Net.Security.RemoteCertificateValidationCallback ServerCertificateValidationCallback
        {
            get
            {
                return _serverCallback;
            }
            set
            {
                _serverCallback = value;
            }
        }
        public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
        {
            get
            {
                return _clientCertificates;
            }
            set
            {
                _clientCertificates = value;
            }
        }

        public bool CheckRevocation
        {
            get
            {
                return _checkRevocation;
            }
            set
            {
                _checkRevocation = value;
            }
        }

        #endregion
    }
#endif
}
