using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CreateMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var assemblyName = new AssemblyName("CreateMap");

            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);

            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            var typeBuilder = moduleBuilder.DefineType("MapAToB", TypeAttributes.Public);
            
            var transformer = typeBuilder.DefineMethod("Transformer", MethodAttributes.Public,
                CallingConventions.HasThis, typeof(B), new[] { typeof(A) });
            var ilMethod = transformer.GetILGenerator();

            ilMethod.Emit(OpCodes.Newobj, typeof(B).GetConstructor(Type.EmptyTypes));
            ilMethod.Emit(OpCodes.Dup);
            ilMethod.Emit(OpCodes.Ldarg_1);
            ilMethod.Emit(OpCodes.Callvirt, typeof(A).GetProperty(nameof(A.X)).GetGetMethod());
            ilMethod.EmitCall(OpCodes.Callvirt, typeof(B).GetProperty(nameof(B.Z)).GetSetMethod(), new []{typeof(string)});
            ilMethod.Emit(OpCodes.Ret);

            var type = typeBuilder.CreateType();

            var method = type.GetMethod("Transformer");

            var trans = Activator.CreateInstance(type);
            var b = method.Invoke(trans, new[] {new A {X = "10"}});
            Console.WriteLine("B.Z={0}", ((B)b).Z);
        }
    }


    public class A
    {
        public string X { get; set; }
    }

    public class B
    {
        
        public string Z { get; set; }
    }

    class MapAToB2
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
