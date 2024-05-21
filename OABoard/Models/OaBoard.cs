namespace OABoard.Models
{
    public class OaBoard
    {

        public string Partnumber { get; set; }
        public double Plan { get; set; }
        public double Actual { get; set; }
        public double Diff { get; set; }
        public double OR { get; set; }

        public double CT { get; set; }

        public Lintestatus LineStatus { get; set; }
        public OaStatus OaStatus { get; set; }
    }


    public enum Lintestatus
    {
        Operate,
        Break,
        Stop
    }

    public enum OaStatus
    {
        Morethan,
        Lessthan
    }
}
