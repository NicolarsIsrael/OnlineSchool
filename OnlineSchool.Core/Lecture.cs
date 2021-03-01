namespace OnlineSchool.Core
{
    public class Lecture : Entity
    {
        public string Title { get; set; }
        public string BriefDescription { get; set; }
        public string FilePath { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}