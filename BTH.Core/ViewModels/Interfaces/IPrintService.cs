using BHT.Core.Entities;

namespace BTH.Core.ViewModels.Interfaces
{
    public interface IPrintService
    {
        void Print(CoBaTransaction[] transactions);
    }
}
