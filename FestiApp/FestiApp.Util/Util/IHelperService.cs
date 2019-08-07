using System.Threading.Tasks;
using FestiAppViewModels;

namespace FestiApp.Util.Util
{
    public interface IHelperService 
    {
        Task<DistanceViewModel> GetDistanceBetweenTwoLocatioins(string fromx, string fromy, string tox, string toy,
            NetworkType transporttype = NetworkType.Auto, Format f = Format.MinKm);
        Task<DistanceViewModel> GetDistanceBetweenTwoLocatioinsByAdresId(string adresid1, string adresid2,
            NetworkType transporttype = NetworkType.Auto, Format format = Format.MinKm);
        Task<Doc> GetDocByAdres(string city = null, string street = null, string housenumber = null, string postalcode = null);
    }
}