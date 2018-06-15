**Project Description**

MailSystem is a suite of .NET components that provide users with an extensive set of email tools. MailSystem provides full support for SMTP, POP3, IMAP4, NNTP, MIME, S/MIME, OpenPGP, DNS, vCard, vCalendar, Anti-Spam (Bayesian , RBL, DomainKeys), Queueing, Mail Merge and WhoIs

This project licensed under LGPL, you are free to use the compiled binaries in your personal or commercial project for free. If, for some reasons, you want to keep your changes for yourself, you must acquire a [commercial license](http://www.agilecomponents.com/).


**Common features:**

 - Asynchronous operations
 - Secure connections using SSL
 - Mail signing & encryption/decryption using S/MIME & OpenPGP
 - Separate object for the Message and the clients
 - Fully Accessible Headers
 - Mail encoding customization for globalization
 - Determine if it is a mail server bounce + e-mail address parsing
 - Send from / receive Message object to file and streams

**SMTP - Simple Mail Transfer Protocol**

 - Direct sending of e-mail without an SMTP server
 - MX server caching (increase by 6 the performance of direct send)
 - Mail queueing systems support
 - Multiple bodies (both HTML and Text)
 - Embedded Objects such as Image, Sound and Video
 - Redundant SMTP Server List (fail over)
 - Secure Authentication

**POP3 - Post Office Protocol 3**

 - View Mailbox Size and Message Count
 - Download Full Message or Header Only
 - Secure Authentication

**IMAP4 - Internet Message Access Protocol 4**

 - Manage Mailboxes (list, create, rename, delete, move, empty, etc.)
 - Manage Messages (retrieve, copy, delete, append, etc.)
 - Download Full Message or Header Only
 - Manage flags
 - Extended search features
 - Secure Authentication
 - New message arrival notification support (IDLE command)

**NNTP - Network News Transfer Protocol**

 - List News groups and Articles Easily
 - Download Full Articles or only Headers
 - Secure Authentication
 - Mail merging and template
 - Load Configuration from XML or Text File
 - Field formatting
 - Bind Bodies with Data Sources (DataTable, ArrayList, Custom collections, ...)
 - Bulk Mailing from a Data Source (in addition to the multiple data bound bodies)

**vCard & vCalendar**

 - Contact file reading and writing
 - Calendar event reading and writing
 - Sending and receiving a meeting request

**Anti-Spam**

 - Block list servers support (RBL)
 - Full DomainKeys implementation
 - Learning Bayesian filter
 - Email Addresses Validation Using MX Record Caching
 - Commtouch® anti-spam technology support

**DNS**

 - Support for individual queries of all types
 - Get mail exchange (MX) records of a DNS server
 - Get all records from a DNS server

**WhoIs**

 - WhoIs server querying
 - Asynchronous mode
 - Domain name availability check
 - Custom WhoIs server list resource file support

**Mail queueing application**

In addition to the library, the suite provides the developer with a full featured mail queueing system called ActiveUp.Q that is completed integrated with the solution. This advanced queueing solution includes but is not limited to:

 - Standard or scheduled queueing of complex e-mails
 - Multiple pickup directory support (ideal for ISP's)
 - Automatic Thread load-balancing
 - Powerful service managing to monitor the running and scheduled tasks
 - Fault-tolerant and auto-recovery
 - Execute a GET or POST query on any HTTP web site or intranet
 - Triggers include Daily, Weekly, Monthly or a specific day, week and month of year
 - Multiple XML task list configuration file support

[![Powered by ndepend](PoweredByNDepend.png)](https://www.ndepend.com/)

**Who's using this**

MailSystem is in production use at: [Jitbit Helpdesk](https://www.jitbit.com/helpdesk/), [SiteCore.net](http://www.sitecore.net/)

**Last trunk build status**

![Last trunk build status](https://ci.appveyor.com/api/projects/status/k31h22sukrym6ys4?svg=true)
