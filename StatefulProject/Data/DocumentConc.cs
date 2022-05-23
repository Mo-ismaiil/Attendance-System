namespace StatefulProject.Data
{
    public class DocumentConc:IDocument
    {
        private ApplicationDbContext Context { get; set; }
        public DocumentConc(ApplicationDbContext _context)
        {
            Context = _context;
        }
        public List<Document> getDocuments()
        {
            return Context.Documents.ToList();
        }
        public Document getDocumentByStudentId(int StudentId)
        {
            var doc = Context.Documents.FirstOrDefault(a => a.StudentId == StudentId);

            return doc;
        }
        public void updateDocument(int StudentId, Document updatedDoc)
        {
            var oldDoc = getDocumentByStudentId(StudentId);
            var x = updatedDoc;
            oldDoc.StudentId = updatedDoc.StudentId;
            oldDoc.Graduation = updatedDoc.Graduation;
            oldDoc.IdImg = updatedDoc.IdImg;
            oldDoc.Military = updatedDoc.Military;
            oldDoc.Notes = updatedDoc.Notes;
            oldDoc.PolicePaper = updatedDoc.PolicePaper;
            oldDoc.BirthDate = updatedDoc.BirthDate;
            oldDoc.Contract = updatedDoc.Contract;
            Context.SaveChanges();
        }
    }
}
