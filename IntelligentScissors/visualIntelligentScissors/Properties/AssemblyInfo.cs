using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("visualIntelligentScissors")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("visualIntelligentScissors")]
[assembly: AssemblyCopyright("Copyright ©  2005")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: UIPermission(SecurityAction.RequestMinimum, Window=UIPermissionWindow.SafeTopLevelWindows)] // we have a GUI
[assembly: FileDialogPermission(SecurityAction.RequestMinimum, Open = true, Save = true)] // file open/save dialogs
[assembly: UIPermission(SecurityAction.RequestOptional, Window = UIPermissionWindow.AllWindows)] // gets rid of the disclaimer on title bar
[assembly: UIPermission(SecurityAction.RequestOptional, Clipboard=UIPermissionClipboard.AllClipboard)] // copy out segment points
[assembly: SecurityPermission(SecurityAction.RequestOptional, UnmanagedCode=true)] // required for marshaling optimization

// So that we can use C5 library
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Name = "FullTrust")]



// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b85d887c-a266-4080-b2c9-212039c66473")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
