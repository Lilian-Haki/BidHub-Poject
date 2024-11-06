namespace BidHub_Poject.DTO
{
    public class ProductsDTO
    {
        public string ProductName { get; set; }
        public string ReasonForAuction { get; set; }
        public string OwnersName { get; set; }
        public string OwnerPhoneNo { get; set; }
        public double ReservePrice { get; set; }
        public string Location { get; set; }
        public List<ProductDocDTO> Documents { get; set; }
        public List<ProductPhotosDTO> Photos { get; set; }
    }
}
