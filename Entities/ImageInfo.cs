namespace MyProj.Entities;

public class ImageInfo {

    public int Id { get; set; }
    public string FileId { get; set; }
    public string FromWho { get; set; }

    public ImageInfo(string fileId, string fromWho) { FileId = fileId; FromWho = fromWho; } 

}