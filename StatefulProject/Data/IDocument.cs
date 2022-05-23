
namespace StatefulProject.Data
{
    public interface IDocument
    {
        public List<Document> getDocuments();
        public Document getDocumentByStudentId(int id);
        public void updateDocument(int id, Document old);
    }
}
