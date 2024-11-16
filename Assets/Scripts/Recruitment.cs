using System.Linq;
using UnityEngine;

public class Recruitment : MonoBehaviour
{

    public AudiController audio;
    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("reward").GetComponent<AudiController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("add"))
        {
            other.gameObject.tag = "Finish";
            PlayerManager.PlayerManagerCls.Rblst.Add(other.collider.GetComponent<Rigidbody>());

            other.transform.parent = null;

            other.transform.parent = PlayerManager.PlayerManagerCls.transform;

            other.gameObject.GetComponent<memeberManager>().member = true;

            if (!other.collider.gameObject.GetComponent<Recruitment>())
            {
                other.collider.gameObject.AddComponent<Recruitment>();
            }

            other.collider.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material =
                PlayerManager.PlayerManagerCls.Rblst.ElementAt(0).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
            
            
        }

        if(other.collider.CompareTag("obstacle"))
        {
            if (PlayerPrefs.GetInt("sound") == 1) audio.OnBlooding();
            PlayerManager.PlayerManagerCls.Rblst.Remove(other.collider.GetComponent<Rigidbody>());
            PlayerManager.PlayerManagerCls.Rblst.Remove(this.gameObject.GetComponent<Rigidbody>());
            this.gameObject.SetActive(false);
        }
    }
}
