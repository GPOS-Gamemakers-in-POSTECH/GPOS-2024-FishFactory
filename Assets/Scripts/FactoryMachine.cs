public class FactoryMachine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        willMakeMachine(int Tier);
    }

    // Make factory machine according to the factory tier
    func willMakeMachine(int Tier)
    {
        switch (Tier)
        {
            case 2:
                if willMakeFreezer() {
                    Freezer = 1; // Naengdong-gi
                }
                if willMakePackagingMachine() {
                    PackagingMachine = 1; // Pojang-gi
                }
                if willMakeDryer() {
                    Dryer = 1; // Geonjogi
                }
                break;
            case 3:
                if willMakeFreezer() {
                    Freezer = 1; // Naengdong-gi
                }
                if willMakePackagingMachine() {
                    PackagingMachine = 1; // Pojang-gi
                }
                if willMakeDryer() {
                    Dryer = 1; // Geonjogi
                }
                if willMakeBoneBreaker() {
                    BoneBreaker = 1; // Balgolgi
                }
                if willMakeSaltedFishCharger() {
                    SaltedFishCharger = 1; // JeotgalChungjun-gi
                }
                break;
            case 4:
                if willMakeFreezer() {
                    Freezer = 1; // Naengdong-gi
                }
                if willMakePackagingMachine() {
                    PackagingMachine = 1; // Pojang-gi
                }
                if willMakeDryer() {
                    Dryer = 1; // Geonjogi
                }
                if willMakeBoneBreaker() {
                    BoneBreaker = 1; // Balgolgi
                }
                if willMakeSaltedFishCharger() {
                    SaltedFishCharger = 1; // JeotgalChungjun-gi
                }
                if willMakeStarchSynthesizer() {
                    StarchSynthesizer = 1; // JeonbunHapseong-gi
                }
                if willMakeFryer() {
                    Fryer = 1; // Tuigimgi
                }
                break;
            case 5:
                if willMakeFreezer() {
                    Freezer = 1; // Naengdong-gi
                }
                if willMakePackagingMachine() {
                    PackagingMachine = 1; // Pojang-gi
                }
                if willMakeDryer() {
                    Dryer = 1; // Geonjogi
                }
                if willMakeBoneBreaker() {
                    BoneBreaker = 1; // Balgolgi
                }
                if willMakeSaltedFishCharger() {
                    SaltedFishCharger = 1; // JeotgalChungjun-gi
                }
                if willMakeStarchSynthesizer() {
                    StarchSynthesizer = 1; // JeonbunHapseong-gi
                }
                if willMakeFryer() {
                    Fryer = 1; // Tuigimgi
                }
                if willMakeCanCharger() {
                    CanCharger = 1; // TongjorimChungjeon-gi
                }
                if willMakeCanMaker() {
                    CanMaker = 1; // TongjorimJejak-gi
                }
                break;
        }
    }

    class ProcessMachine
    {
        var GeneralSale = 1; // General sale mean
        var Freezing, Drying, SaltedFish, FishCake, Can: bool

        if Freezer == 1 && PackagingMachine == 1: Freezing = 1; // Naengdong active
        if Dryer == 1 && PackagingMachine == 1: Drying = 1; // Geonjo active
        if BoneBreaker == 1 && SaltedFishCharger == 1 && PackagingMachine == 1: SaltedFish = 1; // Jeotgal active
        if BoneBreaker == 1 && StarchSynthesizer == 1 && Fryer == 1 && PackagingMachine == 1: FishCake = 1; // Eomuk active
        if BoneBreaker == 1 && CanCharger == 1 && CanMaker == 1 && PackagingMachine == 1: Can = 1;
    }
        

}
