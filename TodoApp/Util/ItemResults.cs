using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TodoApp.Util
{
    public class ItemResults<T> where T: class
    {
        public ItemResults()
        {
            Data = new List<T>();
        }
        public int Total { get; set; }
        public IList<T> Data { get; set; }
    }
}
