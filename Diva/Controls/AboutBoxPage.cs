﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Diva.Utilities;

namespace Diva.Controls
{
    public partial class ConfigureForm : Form
    {
        static readonly string ApacheLicenseV2Name = "Apache License Version 2.0";
        static readonly string ApacheLicenseV2Terms = @"                                 Apache License
                           Version 2.0, January 2004
                        http://www.apache.org/licenses/
 
   TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION
 
   1. Definitions.
 
      ""License"" shall mean the terms and conditions for use, reproduction,
      and distribution as defined by Sections 1 through 9 of this document.
 
      ""Licensor"" shall mean the copyright owner or entity authorized by
      the copyright owner that is granting the License.
 
      ""Legal Entity"" shall mean the union of the acting entity and all
      other entities that control, are controlled by, or are under common
      control with that entity. For the purposes of this definition,
      ""control"" means (i) the power, direct or indirect, to cause the
      direction or management of such entity, whether by contract or
      otherwise, or (ii) ownership of fifty percent (50%) or more of the
      outstanding shares, or (iii) beneficial ownership of such entity.
 
      ""You"" (or ""Your"") shall mean an individual or Legal Entity
      exercising permissions granted by this License.
 
      ""Source"" form shall mean the preferred form for making modifications,
      including but not limited to software source code, documentation
      source, and configuration files.
 
      ""Object"" form shall mean any form resulting from mechanical
      transformation or translation of a Source form, including but
      not limited to compiled object code, generated documentation,
      and conversions to other media types.
 
      ""Work"" shall mean the work of authorship, whether in Source or
      Object form, made available under the License, as indicated by a
      copyright notice that is included in or attached to the work
      (an example is provided in the Appendix below).
 
      ""Derivative Works"" shall mean any work, whether in Source or Object
      form, that is based on (or derived from) the Work and for which the
      editorial revisions, annotations, elaborations, or other modifications
      represent, as a whole, an original work of authorship. For the purposes
      of this License, Derivative Works shall not include works that remain
      separable from, or merely link (or bind by name) to the interfaces of,
      the Work and Derivative Works thereof.
 
      ""Contribution"" shall mean any work of authorship, including
      the original version of the Work and any modifications or additions
      to that Work or Derivative Works thereof, that is intentionally
      submitted to Licensor for inclusion in the Work by the copyright owner
      or by an individual or Legal Entity authorized to submit on behalf of
      the copyright owner. For the purposes of this definition, ""submitted""
      means any form of electronic, verbal, or written communication sent
      to the Licensor or its representatives, including but not limited to
      communication on electronic mailing lists, source code control systems,
      and issue tracking systems that are managed by, or on behalf of, the
      Licensor for the purpose of discussing and improving the Work, but
      excluding communication that is conspicuously marked or otherwise
      designated in writing by the copyright owner as ""Not a Contribution.""
 
      ""Contributor"" shall mean Licensor and any individual or Legal Entity
      on behalf of whom a Contribution has been received by Licensor and
      subsequently incorporated within the Work.
 
   2. Grant of Copyright License. Subject to the terms and conditions of
      this License, each Contributor hereby grants to You a perpetual,
      worldwide, non-exclusive, no-charge, royalty-free, irrevocable
      copyright license to reproduce, prepare Derivative Works of,
      publicly display, publicly perform, sublicense, and distribute the
      Work and such Derivative Works in Source or Object form.
 
   3. Grant of Patent License. Subject to the terms and conditions of
      this License, each Contributor hereby grants to You a perpetual,
      worldwide, non-exclusive, no-charge, royalty-free, irrevocable
      (except as stated in this section) patent license to make, have made,
      use, offer to sell, sell, import, and otherwise transfer the Work,
      where such license applies only to those patent claims licensable
      by such Contributor that are necessarily infringed by their
      Contribution(s) alone or by combination of their Contribution(s)
      with the Work to which such Contribution(s) was submitted. If You
      institute patent litigation against any entity (including a
      cross-claim or counterclaim in a lawsuit) alleging that the Work
      or a Contribution incorporated within the Work constitutes direct
      or contributory patent infringement, then any patent licenses
      granted to You under this License for that Work shall terminate
      as of the date such litigation is filed.
 
   4. Redistribution. You may reproduce and distribute copies of the
      Work or Derivative Works thereof in any medium, with or without
      modifications, and in Source or Object form, provided that You
      meet the following conditions:
 
      (a) You must give any other recipients of the Work or
          Derivative Works a copy of this License; and
 
      (b) You must cause any modified files to carry prominent notices
          stating that You changed the files; and
 
      (c) You must retain, in the Source form of any Derivative Works
          that You distribute, all copyright, patent, trademark, and
          attribution notices from the Source form of the Work,
          excluding those notices that do not pertain to any part of
          the Derivative Works; and
 
      (d) If the Work includes a ""NOTICE"" text file as part of its
          distribution, then any Derivative Works that You distribute must
          include a readable copy of the attribution notices contained
          within such NOTICE file, excluding those notices that do not
          pertain to any part of the Derivative Works, in at least one
          of the following places: within a NOTICE text file distributed
          as part of the Derivative Works; within the Source form or
          documentation, if provided along with the Derivative Works; or,
          within a display generated by the Derivative Works, if and
          wherever such third-party notices normally appear. The contents
          of the NOTICE file are for informational purposes only and
          do not modify the License. You may add Your own attribution
          notices within Derivative Works that You distribute, alongside
          or as an addendum to the NOTICE text from the Work, provided
          that such additional attribution notices cannot be construed
          as modifying the License.
 
      You may add Your own copyright statement to Your modifications and
      may provide additional or different license terms and conditions
      for use, reproduction, or distribution of Your modifications, or
      for any such Derivative Works as a whole, provided Your use,
      reproduction, and distribution of the Work otherwise complies with
      the conditions stated in this License.
 
   5. Submission of Contributions. Unless You explicitly state otherwise,
      any Contribution intentionally submitted for inclusion in the Work
      by You to the Licensor shall be under the terms and conditions of
      this License, without any additional terms or conditions.
      Notwithstanding the above, nothing herein shall supersede or modify
      the terms of any separate license agreement you may have executed
      with Licensor regarding such Contributions.
 
   6. Trademarks. This License does not grant permission to use the trade
      names, trademarks, service marks, or product names of the Licensor,
      except as required for reasonable and customary use in describing the
      origin of the Work and reproducing the content of the NOTICE file.
 
   7. Disclaimer of Warranty. Unless required by applicable law or
      agreed to in writing, Licensor provides the Work (and each
      Contributor provides its Contributions) on an ""AS IS"" BASIS,
      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
      implied, including, without limitation, any warranties or conditions
      of TITLE, NON-INFRINGEMENT, MERCHANTABILITY, or FITNESS FOR A
      PARTICULAR PURPOSE. You are solely responsible for determining the
      appropriateness of using or redistributing the Work and assume any
      risks associated with Your exercise of permissions under this License.
 
   8. Limitation of Liability. In no event and under no legal theory,
      whether in tort (including negligence), contract, or otherwise,
      unless required by applicable law (such as deliberate and grossly
      negligent acts) or agreed to in writing, shall any Contributor be
      liable to You for damages, including any direct, indirect, special,
      incidental, or consequential damages of any character arising as a
      result of this License or out of the use or inability to use the
      Work (including but not limited to damages for loss of goodwill,
      work stoppage, computer failure or malfunction, or any and all
      other commercial damages or losses), even if such Contributor
      has been advised of the possibility of such damages.
 
   9. Accepting Warranty or Additional Liability. While redistributing
      the Work or Derivative Works thereof, You may choose to offer,
      and charge a fee for, acceptance of support, warranty, indemnity,
      or other liability obligations and/or rights consistent with this
      License. However, in accepting such obligations, You may act only
      on Your own behalf and on Your sole responsibility, not on behalf
      of any other Contributor, and only if You agree to indemnify,
      defend, and hold each Contributor harmless for any liability
      incurred by, or claims asserted against, such Contributor by reason
      of your accepting any such warranty or additional liability.
 
   END OF TERMS AND CONDITIONS
 
   APPENDIX: How to apply the Apache License to your work.
 
      To apply the Apache License to your work, attach the following
      boilerplate notice, with the fields enclosed by brackets ""[]""
      replaced with your own identifying information. (Don't include
      the brackets!)  The text should be enclosed in the appropriate
      comment syntax for the file format. We also recommend that a
      file or class name and description of purpose be included on the
      same ""printed page"" as the copyright notice for easier
      identification within third-party archives.
 
   Copyright [yyyy] [name of copyright owner]
 
   Licensed under the Apache License, Version 2.0 (the ""License"");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
 
       http://www.apache.org/licenses/LICENSE-2.0
 
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an ""AS IS"" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.";
        static readonly string ApacheLicenseV2Template = @"Copyright(c) {0} {1}

Licensed under the Apache License, Version 2.0 (the ""License""); you
may not use this file except in compliance with the License.You may
obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
            software
distributed under the License is distributed on an ""AS IS"" BASIS,
            WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
            either express or
implied.See the License for the specific language governing permissions
and limitations under the License.";

        static readonly string MITLicenseName = "The MIT License";
        static readonly string MITLicenseTemplate = @"The MIT License

Copyright (c) {0} {1}

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"",
            WITHOUT WARRANTY OF ANY KIND,
            EXPRESS OR IMPLIED,
            INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
            FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
            DAMAGES OR OTHER LIABILITY,
            WHETHER IN AN ACTION OF CONTRACT,
            TORT OR OTHERWISE,
            ARISING FROM,
            OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";

        class License
        {
            public string LicenseName;
            public string LicenseURL;
            public string LicenseText;
        }

        static License ApacheLicenseV2 = new License
        {
            LicenseName = ApacheLicenseV2Name,
            LicenseURL = "http://www.apache.org/licenses/LICENSE-2.0",
            LicenseText = ApacheLicenseV2Terms
        };

        static License MITLicense = new License
        {
            LicenseName = MITLicenseName,
            LicenseURL = "https://opensource.org/licenses/MIT",
            LicenseText = @"The MIT License
SPDX short identifier: MIT

Copyright <YEAR> <COPYRIGHT HOLDER>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"",
            WITHOUT WARRANTY OF ANY KIND,
            EXPRESS OR IMPLIED,
            INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
            FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
            DAMAGES OR OTHER LIABILITY,
            WHETHER IN AN ACTION OF CONTRACT,
            TORT OR OTHERWISE,
            ARISING FROM,
            OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE."
        };

       struct Credit
        {
            public string ProjectName;
            public string ProjectURL;
            public License License;
        }

        static Credit[] Credits = new Credit[]
        {
            new Credit
            {
                ProjectName = "Dapper",
                ProjectURL = "https://github.com/StackExchange/Dapper",
                License = ApacheLicenseV2
            },
            new Credit
            {
                ProjectName = "EntityFramework",
                ProjectURL = "https://github.com/aspnet/EntityFramework6",
                License = new License
                {
                    LicenseName = "Apache License Version 2.0",
                    LicenseURL = "https://github.com/aspnet/EntityFramework6/blob/master/License.txt",
                    LicenseText = ApacheLicenseV2Template.FormatWith(@"Microsoft Open Technologies, Inc.  All rights reserved.
Microsoft Open Technologies would like to thank its contributors, a list of whom
are at http://aspnetwebstack.codeplex.com/wikipage?title=Contributors.", "")
                }
            },
            new Credit
            {
                ProjectName = "GMap.NET",
                ProjectURL = "http://greatmaps.codeplex.com/",
                License = new License
                {
                    LicenseName = "Flat Earth License",
                    LicenseURL = "https://github.com/radioman/greatmaps/blob/master/Info/License.txt",
                    LicenseText = @"                    >>> FLAT EARTH LICENSE <<<
-------------------------------------------------------------------
Free of charge, to any person obtaining a copy of this software
and associated documentation files (the ""Software""), to deal in
the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute,
sublicense, and/or sell copies of the Software."
                }
            },
            new Credit
            {
                ProjectName = "Json.NET",
                ProjectURL = "https://www.newtonsoft.com/json",
                License = new License
                {
                    LicenseName = MITLicenseName,
                    LicenseURL = "https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md",
                    LicenseText = MITLicenseTemplate.FormatWith("2007", "James Newton-King")
                }
            },
            new Credit
            {
                ProjectName = "log4net",
                ProjectURL = "http://logging.apache.org/log4net/",
                License = new License
                {
                    LicenseName = "Apache License Version 2.0",
                    LicenseURL = "http://logging.apache.org/log4net/license.html",
                    LicenseText = ApacheLicenseV2Terms
                }
            },
            new Credit
            {
                ProjectName = "NDesk.Options",
                ProjectURL = "http://www.ndesk.org/Options",
                License = MITLicense
            },
            new Credit
            {
                ProjectName = "Pymavlink",
                ProjectURL = "https://github.com/ArduPilot/pymavlink",
                License = new License
                {
                    LicenseName = "LGPLv3",
                    LicenseURL = "https://github.com/ArduPilot/pymavlink/blob/master/COPYING",
                    LicenseText = @"This repository contains the generator for the MAVLink protocol. The generator itself is
(L)GPL v3 licensed, while the generated code is subject to different licenses:


========================================================================================

                        Exception to the (L)GPL v.3:

As an exception, if you use this Software to compile your source code and
portions of this Software, including messages (""the generator output""), are embedded
into the binary product as a result, you may redistribute such product and such
product is hereby licensed under the following MIT license:

Permission is hereby granted, free of charge, to any person obtaining a copy
of the generated software (the ""Generated Software""), to deal
in the Generated Software without restriction, including without limitation the
rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Generated Software, and to permit persons to whom the Generated
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Generated Software.

THE GENERATED SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS
OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE GENERATED SOFTWARE OR THE USE OR OTHER DEALINGS
IN THE GENERATED SOFTWARE.

=========================================================================================



                   GNU LESSER GENERAL PUBLIC LICENSE
                       Version 3, 29 June 2007

 Copyright (C) 2007 Free Software Foundation, Inc. <http://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.


  This version of the GNU Lesser General Public License incorporates
the terms and conditions of version 3 of the GNU General Public
License, supplemented by the additional permissions listed below.

  0. Additional Definitions.

  As used herein, ""this License"" refers to version 3 of the GNU Lesser
General Public License, and the ""GNU GPL"" refers to version 3 of the GNU
General Public License.

  ""The Library"" refers to a covered work governed by this License,
other than an Application or a Combined Work as defined below.

  An ""Application"" is any work that makes use of an interface provided
by the Library, but which is not otherwise based on the Library.
Defining a subclass of a class defined by the Library is deemed a mode
of using an interface provided by the Library.

  A ""Combined Work"" is a work produced by combining or linking an
Application with the Library.  The particular version of the Library
with which the Combined Work was made is also called the ""Linked
Version"".

  The ""Minimal Corresponding Source"" for a Combined Work means the
Corresponding Source for the Combined Work, excluding any source code
for portions of the Combined Work that, considered in isolation, are
based on the Application, and not on the Linked Version.

  The ""Corresponding Application Code"" for a Combined Work means the
object code and/or source code for the Application, including any data
and utility programs needed for reproducing the Combined Work from the
Application, but excluding the System Libraries of the Combined Work.

  1. Exception to Section 3 of the GNU GPL.

  You may convey a covered work under sections 3 and 4 of this License
without being bound by section 3 of the GNU GPL.

  2. Conveying Modified Versions.

  If you modify a copy of the Library, and, in your modifications, a
facility refers to a function or data to be supplied by an Application
that uses the facility (other than as an argument passed when the
facility is invoked), then you may convey a copy of the modified
version:

   a) under this License, provided that you make a good faith effort to
   ensure that, in the event an Application does not supply the
   function or data, the facility still operates, and performs
   whatever part of its purpose remains meaningful, or

   b) under the GNU GPL, with none of the additional permissions of
   this License applicable to that copy.

  3. Object Code Incorporating Material from Library Header Files.

  The object code form of an Application may incorporate material from
a header file that is part of the Library.  You may convey such object
code under terms of your choice, provided that, if the incorporated
material is not limited to numerical parameters, data structure
layouts and accessors, or small macros, inline functions and templates
(ten or fewer lines in length), you do both of the following:

   a) Give prominent notice with each copy of the object code that the
   Library is used in it and that the Library and its use are
   covered by this License.

   b) Accompany the object code with a copy of the GNU GPL and this license
   document.

  4. Combined Works.

  You may convey a Combined Work under terms of your choice that,
taken together, effectively do not restrict modification of the
portions of the Library contained in the Combined Work and reverse
engineering for debugging such modifications, if you also do each of
the following:

   a) Give prominent notice with each copy of the Combined Work that
   the Library is used in it and that the Library and its use are
   covered by this License.

   b) Accompany the Combined Work with a copy of the GNU GPL and this license
   document.

   c) For a Combined Work that displays copyright notices during
   execution, include the copyright notice for the Library among
   these notices, as well as a reference directing the user to the
   copies of the GNU GPL and this license document.

   d) Do one of the following:

       0) Convey the Minimal Corresponding Source under the terms of this
       License, and the Corresponding Application Code in a form
       suitable for, and under terms that permit, the user to
       recombine or relink the Application with a modified version of
       the Linked Version to produce a modified Combined Work, in the
       manner specified by section 6 of the GNU GPL for conveying
       Corresponding Source.

       1) Use a suitable shared library mechanism for linking with the
       Library.  A suitable mechanism is one that (a) uses at run time
       a copy of the Library already present on the user's computer
       system, and (b) will operate properly with a modified version
       of the Library that is interface-compatible with the Linked
       Version.

   e) Provide Installation Information, but only if you would otherwise
   be required to provide such information under section 6 of the
   GNU GPL, and only to the extent that such information is
   necessary to install and execute a modified version of the
   Combined Work produced by recombining or relinking the
   Application with a modified version of the Linked Version. (If
   you use option 4d0, the Installation Information must accompany
   the Minimal Corresponding Source and Corresponding Application
   Code. If you use option 4d1, you must provide the Installation
   Information in the manner specified by section 6 of the GNU GPL
   for conveying Corresponding Source.)

  5. Combined Libraries.

  You may place library facilities that are a work based on the
Library side by side in a single library together with other library
facilities that are not Applications and are not covered by this
License, and convey such a combined library under terms of your
choice, if you do both of the following:

   a) Accompany the combined library with a copy of the same work based
   on the Library, uncombined with any other library facilities,
   conveyed under the terms of this License.

   b) Give prominent notice with the combined library that part of it
   is a work based on the Library, and explaining where to find the
   accompanying uncombined form of the same work.

  6. Revised Versions of the GNU Lesser General Public License.

  The Free Software Foundation may publish revised and/or new versions
of the GNU Lesser General Public License from time to time. Such new
versions will be similar in spirit to the present version, but may
differ in detail to address new problems or concerns.

  Each version is given a distinguishing version number. If the
Library as you received it specifies that a certain numbered version
of the GNU Lesser General Public License ""or any later version""
applies to it, you have the option of following the terms and
conditions either of that published version or of any later version
published by the Free Software Foundation. If the Library as you
received it does not specify a version number of the GNU Lesser
General Public License, you may choose any version of the GNU Lesser
General Public License ever published by the Free Software Foundation.

  If the Library as you received it specifies that a proxy can decide
whether future versions of the GNU Lesser General Public License shall
apply, that proxy's public statement of acceptance of any version is
permanent authorization for you to choose that version for the
Library."
                }
            },
            new Credit
            {
                ProjectName = "System.Data.SQLie",
                ProjectURL = "https://system.data.sqlite.org/",
                License = new License
                {
                    LicenseName = "Public Domain/Microsoft Public License",
                    LicenseURL = "https://system.data.sqlite.org/index.html/doc/trunk/www/copyright.wiki",
                    LicenseText = @"System.Data.SQLite Copyright
All files in the ""System.Data.SQLite.Linq/SQL Generation"" directory (within the source tree) are covered by the Microsoft Public License (MS-PL). These files end up being compiled into both the ""System.Data.SQLite.Linq"" and ""System.Data.SQLite.EF6"" assemblies.

All other code and documentation in System.Data.SQLite has been dedicated to the public domain by the authors.All code authors, and representatives of the companies they work for, have signed affidavits dedicating their contributions to the public domain and originals of those signed affidavits are stored in a firesafe at the main offices of Hwaci.Anyone is free to copy, modify, publish, use, compile, sell, or distribute the original System.Data.SQLite code, either in source code form or as a compiled binary, for any purpose, commercial or non-commercial, and by any means."
                }
            },
            new Credit
            {
                ProjectName = "System.Reactive",
                ProjectURL = "https://github.com/dotnet/reactive",
                License = new License
                {
                    LicenseName = ApacheLicenseV2Name,
                    LicenseURL = "https://github.com/dotnet/reactive/blob/master/LICENSE",
                    LicenseText = ApacheLicenseV2Template.FormatWith(@".NET Foundation and Contributors
All Rights Reserved", "")
                }
            },
            new Credit
            {
                ProjectName = "Windows API Code Pack 1.1",
                ProjectURL = "https://github.com/aybe/Windows-API-Code-Pack-1.1",
                License = new License
                {
                    LicenseName = "Custom/Microsoft Software License Terms",
                    LicenseURL = "https://github.com/aybe/Windows-API-Code-Pack-1.1/blob/master/LICENCE",
                    LicenseText = @"License: Custom License
MICROSOFT SOFTWARE LICENSE TERMS
MICROSOFT WINDOWS API CODE PACK FOR MICROSOFT .NET FRAMEWORK
___________________________________________________
These license terms are an agreement between Microsoft Corporation (or based on where you live, one of its affiliates) and you. Please read them. They apply to the software named above, which includes the media on which you received it, if any. The terms also apply to any Microsoft
• updates,
• supplements,
• Internet-based services, and 
• support services
for this software, unless other terms accompany those items. If so, those terms apply.
_______________________________________________________________________________________
BY USING THE SOFTWARE, YOU ACCEPT THESE TERMS. IF YOU DO NOT ACCEPT THEM, DO NOT USE THE SOFTWARE.
If you comply with these license terms, you have the rights below.
1. INSTALLATION AND USE RIGHTS.
• You may use any number of copies of the software to design, develop and test your programs that run on a Microsoft Windows operating system.
• This agreement gives you rights to the software only. Any rights to a Microsoft Windows operating system (such as testing pre-release versions of Windows in a live operating environment) are provided separately by the license terms for Windows.
2. ADDITIONAL LICENSING REQUIREMENTS AND/OR USE RIGHTS.
a. Distributable Code. You may modify, copy, and distribute the software, in source or compiled form, to run on a Microsoft Windows operating system.
ii. Distribution Requirements. If you distribute the software, you must
• require distributors and external end users to agree to terms that protect it at least as much as this agreement; 
• if you modify the software and distribute such modified files, include prominent notices in such modified files so that recipients know that they are not receiving the original software;
• display your valid copyright notice on your programs; and
• indemnify, defend, and hold harmless Microsoft from any claims, including attorneys’ fees, related to the distribution or use of your programs or to your modifications to the software.
iii.Distribution Restrictions.You may not
• alter any copyright, trademark or patent notice in the software; 
• use Microsoft’s trademarks in your programs’ names or in a way that suggests your programs come from or are endorsed by Microsoft; 
• include the software in malicious, deceptive or unlawful programs; or
• modify or distribute the source code of the software so that any part of it becomes subject to an Excluded License.An Excluded License is one that requires, as a condition of use, modification or distribution, that
• the code be disclosed or distributed in source code form; or 
• others have the right to modify it.
3. SCOPE OF LICENSE.The software is licensed, not sold.This agreement only gives you some rights to use the software. Microsoft reserves all other rights.Unless applicable law gives you more rights despite this limitation, you may use the software only as expressly permitted in this agreement.
4. EXPORT RESTRICTIONS. The software is subject to United States export laws and regulations. You must comply with all domestic and international export laws and regulations that apply to the software.These laws include restrictions on destinations, end users and end use.For additional information, see<http://www.microsoft.com/exporting>.
5. SUPPORT SERVICES.Because this software is “as is,” we may not provide support services for it.
6. ENTIRE AGREEMENT.This agreement, and the terms for supplements, updates, Internet-based services and support services that you use, are the entire agreement for the software and support services.
7. APPLICABLE LAW.
a.United States.If you acquired the software in the United States, Washington state law governs the interpretation of this agreement and applies to claims for breach of it, regardless of conflict of laws principles.The laws of the state where you live govern all other claims, including claims under state consumer protection laws, unfair competition laws, and in tort.
b.Outside the United States.If you acquired the software in any other country, the laws of that country apply.
8. LEGAL EFFECT.This agreement describes certain legal rights.You may have other rights under the laws of your country.You may also have rights with respect to the party from whom you acquired the software.This agreement does not change your rights under the laws of your country if the laws of your country do not permit it to do so.
9. DISCLAIMER OF WARRANTY.THE SOFTWARE IS LICENSED “AS-IS.” YOU BEAR THE RISK OF USING IT.MICROSOFT GIVES NO EXPRESS WARRANTIES, GUARANTEES OR CONDITIONS.YOU MAY HAVE ADDITIONAL CONSUMER RIGHTS UNDER YOUR LOCAL LAWS WHICH THIS AGREEMENT CANNOT CHANGE.TO THE EXTENT PERMITTED UNDER YOUR LOCAL LAWS, MICROSOFT EXCLUDES THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
10. LIMITATION ON AND EXCLUSION OF REMEDIES AND DAMAGES.YOU CAN RECOVER FROM MICROSOFT AND ITS SUPPLIERS ONLY DIRECT DAMAGES UP TO U.S. $5.00. YOU CANNOT RECOVER ANY OTHER DAMAGES, INCLUDING CONSEQUENTIAL, LOST PROFITS, SPECIAL, INDIRECT OR INCIDENTAL DAMAGES.
This limitation applies to
• anything related to the software, services, content (including code) on third party Internet sites, or third party programs; and
• claims for breach of contract, breach of warranty, guarantee or condition, strict liability, negligence, or other tort to the extent permitted by applicable law.
It also applies even if Microsoft knew or should have known about the possibility of the damages.The above limitation or exclusion may not apply to you because your country may not allow the exclusion or limitation of incidental, consequential or other damages.
Please note: As this software is distributed in Quebec, Canada, some of the clauses in this agreement are provided below in French.
Remarque : Ce logiciel étant distribué au Québec, Canada, certaines des clauses dans ce contrat sont fournies ci-dessous en français.
EXONÉRATION DE GARANTIE.Le logiciel visé par une licence est offert « tel quel ». Toute utilisation de ce logiciel est à votre seule risque et péril. Microsoft n’accorde aucune autre garantie expresse.Vous pouvez bénéficier de droits additionnels en vertu du droit local sur la protection des consommateurs, que ce contrat ne peut modifier. La ou elles sont permises par le droit locale, les garanties implicites de qualité marchande, d’adéquation à un usage particulier et d’absence de contrefaçon sont exclues.
LIMITATION DES DOMMAGES-INTÉRÊTS ET EXCLUSION DE RESPONSABILITÉ POUR LES DOMMAGES. Vous pouvez obtenir de Microsoft et de ses fournisseurs une indemnisation en cas de dommages directs uniquement à hauteur de 5,00 $ US.Vous ne pouvez prétendre à aucune indemnisation pour les autres dommages, y compris les dommages spéciaux, indirects ou accessoires et pertes de bénéfices.
Cette limitation concerne :
• tout ce qui est relié au logiciel, aux services ou au contenu (y compris le code) figurant sur des sites Internet tiers ou dans des programmes tiers ; et
• les réclamations au titre de violation de contrat ou de garantie, ou au titre de responsabilité stricte, de négligence ou d’une autre faute dans la limite autorisée par la loi en vigueur.
Elle s’applique également, même si Microsoft connaissait ou devrait connaître l’éventualité d’un tel dommage.Si votre pays n’autorise pas l’exclusion ou la limitation de responsabilité pour les dommages indirects, accessoires ou de quelque nature que ce soit, il se peut que la limitation ou l’exclusion ci-dessus ne s’appliquera pas à votre égard.
EFFET JURIDIQUE. Le présent contrat décrit certains droits juridiques.Vous pourriez avoir d’autres droits prévus par les lois de votre pays.Le présent contrat ne modifie pas les droits que vous confèrent les lois de votre pays si celles-ci ne le permettent pas."
                }
            }
        };

        private bool TermsBoxShown;
        private float AnimateFactor;
        private Timer AnimateTimer, FadeCheckTimer;
        private LinkLabel TermsInitiator;

        void InitAboutBox()
        {
            TermsBoxShown = false;
            TermsInitiator = null;
            if (AnimateTimer != null)
            {
                AnimateTimer.Enabled = false;
                FadeCheckTimer.Enabled = false;
            }
            else
            {
                AnimateTimer = new Timer();
                AnimateTimer.Interval = 50;
                AnimateTimer.Tick += new EventHandler((o, e) =>
                {
                    TBoxLicenseTerms.Left += (int)(TBoxLicenseTerms.Width * AnimateFactor);
                    if (AnimateFactor > 0 && TBoxLicenseTerms.Left >= 0)
                    {
                        TBoxLicenseTerms.Left = 0;
                        AnimateTimer.Stop();
                        FadeCheckTimer.Start();
                    } else if (AnimateFactor < 0 && TBoxLicenseTerms.Right < 0)
                    {
                        TBoxLicenseTerms.Left = -TBoxLicenseTerms.Width;
                        AnimateTimer.Stop();
                        TermsBoxShown = false;
                    }
                });
                FadeCheckTimer = new Timer();
                FadeCheckTimer.Interval = 200;
                FadeCheckTimer.Tick += new EventHandler((o, e) =>
                {
                    if (!MouseInControl(TBoxLicenseTerms) && !MouseInControl(TermsInitiator))
                    {
                        AnimateFactor = -0.1F;
                        AnimateTimer.Start();
                        FadeCheckTimer.Stop();
                    }
                });
            }
            AnimateFactor = -0.1F;
            TBoxLicenseTerms.Left = -TBoxLicenseTerms.Width;
            CreditsTablePanel.RowCount = Credits.Length + 1;
            CreditsTablePanel.RowStyles.Clear();
            CreditsTablePanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            //CreditsTablePanel.ColumnStyles[1].SizeType = SizeType.AutoSize;
            //CreditsTablePanel.ColumnStyles[0].SizeType = SizeType.AutoSize;
            for (int i = 0; i < Credits.Length;)
            {
                var prj = new LinkLabel
                {
                    Text = Credits[i].ProjectName,
                    LinkColor = Color.Aquamarine,
                    AutoSize = true
                };
                prj.Links[0].LinkData = Credits[i].ProjectURL;
                var lic = new LinkLabel
                {
                    Text = Credits[i].License.LicenseName,
                    LinkColor = Color.Aquamarine,
                    Margin = new Padding(3),
                    AutoSize = true
                };
                lic.Links[0].LinkData = Credits[i].License;
                lic.LinkClicked += LicenseLinkClicked;
                lic.MouseHover += LicenseLabel_MouseHover;
                CreditsTablePanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                CreditsTablePanel.Controls.Add(prj, 0, ++i);
                CreditsTablePanel.Controls.Add(lic, 1, i);
            }
        }

        private bool MouseInControl(Control c) => MouseInControl(c, Cursor.Position);

        private bool MouseInControl(Control c, Point pt)
        {
            pt = c.PointToClient(pt);
            return (pt.X >= 0 && pt.X < c.Width && pt.Y >= 0 && pt.Y < c.Height);
        }

        private void LicenseLinkClicked(Object sender, EventArgs e)
        {
            var link = (sender as LinkLabel)?.Links[0].LinkData as License;
            if (link != null)
                System.Diagnostics.Process.Start(link.LicenseURL);
        }

        private void LicenseLabel_MouseHover(object sender, EventArgs eventArgs)
        {
            if (!MouseInControl(sender as Control))
                return;
            var link = sender as LinkLabel;
            var lic = link?.Links[0].LinkData as License;
            if (!TermsBoxShown || AnimateFactor < 0)
            {
                TermsBoxShown = true;
                int right = TBoxLicenseTerms.Right;
                TBoxLicenseTerms.Width = AboutBoxPanel.Left + link.Left;
                TBoxLicenseTerms.Left = right - TBoxLicenseTerms.Width;
                TermsInitiator = link;
                TBoxLicenseTerms.Text = lic.LicenseText;
                AnimateFactor = 0.1F;
                AnimateTimer.Start();
            }
            else if (TermsInitiator?.Links[0].LinkData as License != lic)
            {
                TBoxLicenseTerms.Text = lic.LicenseText;
            }
        }
    }
}