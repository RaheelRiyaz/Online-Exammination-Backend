namespace OnlineExammination.Domain.Enums
{
    public enum Gender
    {
        Male=1,
        Female=2
    } 
    
    public enum UserRole
    {
        Admin=1,
        Student=2,
    }

    public enum ResultStatus
    {
        Pass = 1,
        Fail = 2,
    }

    public enum Module
    {
        Student=1,
        Admin=2,
        Gallery=3
    }

    public enum FileFormat
    {
        Image=1,
        Video=2,
        Pdf=3,
        Audio=4
    }
}
