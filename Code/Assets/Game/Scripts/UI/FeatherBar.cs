using UnityEngine;

public class FeatherBar : MonoBehaviour
{
    public ProgressBar pb;
    public MainGoose goose;

    void Update()
    {
        pb.BarValue = goose.experience * 100 / goose.feathersToUp ;
    }
}
