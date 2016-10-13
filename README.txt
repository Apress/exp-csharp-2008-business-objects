This is CSLA .NET version 3.6, including support .NET 3.5 SP1.

Version 3.6.0 of the framework is documented in the
Expert C# 2008 Business Objects book from Apress.

Please note: this archive is a special archive created for
the Expert C# 2008 Business Objects book. The official CSLA
.NET download includes more samples, unit tests and other
code. It is likely that the official version is also more
recent, and may vary from the code shown in the book.

For information on installation, book errata, updates to 
the framework and so forth, please visit 
http://www.lhotka.net/cslanet


This zip file contains the following folders:

cslacs\
The CSLA .NET framework code.

ProjectTrackercs\
The ProjectTracker reference application.



For ease of use, you may consider unzipping this archive
into the following folder structure:

C:\Visual Studio Projects\csla\Source\cslacs
C:\Visual Studio Projects\csla\Samples\CslaNet\cs\ProjectTrackercs

Then make sure to build the Csla project before opening
the ProjectTracker solution. This will ensure that the
references to Csla.dll in the ProjectTracker projects
work correctly.

If you do not follow these instructions, you will need to remove
and re-reference Csla.dll from all ProjectTracker projects
before the ProjectTracker solution will build.