using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;

namespace CreateMap
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://sharplab.io/#v2:EYLgHgbALANALiAlgGwD4AEBMBGAsAKCwAIBBAgbwKOqPQGZbsAGIgDSPKIHMBTOAbiIBnPoIC+BCfgLEAQhSo16jFgC0O3UcK1SpMzEQCyAQwAOJACoB7WURBEAkidMAeEjCKyAfAvw1aDLYWAE7GAHZCAGZWwQC2PMEAFCTCVgCuwQDGPACUitSUfv5KAOxEYTwA7p75xYXFDUTqALypGdkAdKy1/mL8tboEBIhhcAmRxtmOzi4WAMoeFgAiPvj1NMtEIeFRMfFJ821Zuf34YkA===
            var assemblyName = new AssemblyName("CreateMap");

            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);

            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            var typeBuilder = moduleBuilder.DefineType("MapAToB", TypeAttributes.Public);
            
            var transformer = typeBuilder.DefineMethod("Transformer", MethodAttributes.Public,
                CallingConventions.HasThis, typeof(B), new[] { typeof(A) });
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public, 
                    CallingConventions.Standard, 
                    Type.EmptyTypes);

            var ilConstructor = constructor.GetILGenerator();
            ilConstructor.Emit(OpCodes.Ldarg_0);
            ilConstructor.Emit(OpCodes.Call, 
                typeof(object).GetMethod(".ctor", BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
            ilConstructor.Emit(OpCodes.Ret);

            var ilMethod = transformer.GetILGenerator();
            ilMethod.Emit(OpCodes.Newobj,
                typeof(B).GetMethod(".ctor", BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
            ilMethod.Emit(OpCodes.Dup);
            ilMethod.Emit(OpCodes.Ldarg_1);
            ilMethod.Emit(OpCodes.Callvirt, typeof(A).GetProperty(nameof(A.X)).GetGetMethod());
            ilMethod.Emit(OpCodes.Callvirt, typeof(B).GetProperty(nameof(B.Z)).SetMethod);
            ilMethod.Emit(OpCodes.Ret);
        }
    }


    class A
    {
        public string X { get; set; }
    }

    class B
    {
        public string Z { get; set; }
    }

    class MapAToB
    {
        B Transformer(A source)
        {
            return new B
            {
                Z = source.X
            };
        }
    }
}
