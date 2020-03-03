namespace PartyRSVP.Models
{
    public class GuestResponse {
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string Phone {get; set;}
        public string Email {get; set;}
        public string LastName {get; set;}
        public bool? WillAttend {get; set;}

        public string FullName() {
            return this.FirstName + " " + this.LastName;
        }

    }
}