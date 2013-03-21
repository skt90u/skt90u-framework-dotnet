namespace JUtil.CodeGenerator
{
    public interface ICSharpFile
    {
        string USING_NAMESPACES { get; }

        string GetNameSpace();

        string GetClass();
        
    }
}
