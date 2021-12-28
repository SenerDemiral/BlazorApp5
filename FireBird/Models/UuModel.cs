
namespace FireBird.Models
{
    public class UUmodel
    {
        public int FrtID { get; set; }
        public string AdN { get; set; }
        public string Ad { get; set; }


        //public UUmodel(int frtID, string adN, string ad)
        //{
        //    FrtID = frtID;
        //    AdN = adN.Trim();
        //    Ad = ad.Trim();
        //}
        public override string ToString() => $"{AdN} #{FrtID}";


    }
}
