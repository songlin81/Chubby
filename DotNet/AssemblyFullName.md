Getting the full name of an assembly


1. The main problematic part of a full-name is usually the public key token.
You can obtain it by entering this into the Visual Studio Command Prompt Window:

    sn -T path\to\your.dll


2. I think you're looking for System.Reflection.AssemblyName.GetAssemblyName:

AssemblyName name = AssemblyName.GetAssemblyName(@"C:\path\assembly.dll");
Console.WriteLine(name.FullName);
