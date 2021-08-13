using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Kill : MonoBehaviour
{
    [SerializeField] private float RayLength;
    [SerializeField] private AudioSource soundtrack;
    [SerializeField] private GameObject restartMenu;
    AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    bool isDead = false;

    float RayPositionOffset;
    Vector3 RayPositionCenterVertical;
    Vector3 RayPositionLeftVertical;
    Vector3 RayPositionRightVertical;
    Vector3 RayPositionCenterHorizontal;
    Vector3 RayPositionHighHorizontal;
    Vector3 RayPositionLowHorizontal;

    RaycastHit2D[] CeilingHitsCenter;
    RaycastHit2D[] CeilingHitsLeft;
    RaycastHit2D[] CeilingHitsRight;

    RaycastHit2D[] WallHitsCenter;
    RaycastHit2D[] WallHitsLow;
    RaycastHit2D[] WallHitsHigh;

    RaycastHit2D[][] AllRaycastHits = new RaycastHit2D[6][];
    Animator animator;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        RayPositionOffset = (collider.size.y / 2) - 0.2f;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Death()
    {
        isDead = true;
        animator.Play("Death");
        soundtrack.mute = true;
        audioSource.PlayOneShot(clip);
        restartMenu.SetActive(true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    bool shouldDie(RaycastHit2D[][] AllRaycastHits)
    {
        foreach (RaycastHit2D[] HitList in AllRaycastHits)
            foreach (RaycastHit2D hit in HitList)
                if (hit.collider != null)
                    if (hit.collider.tag == "Block")
                        return true;
        return false;
    }

    void DeathControl()
    {
        // Calculate ray starting points
        RayPositionCenterVertical = transform.position + new Vector3(0, collider.size.y / 2, 0);
        RayPositionLeftVertical = transform.position + new Vector3(-RayPositionOffset, collider.size.y / 2, 0);
        RayPositionRightVertical = transform.position + new Vector3(RayPositionOffset, collider.size.y / 2, 0);

        RayPositionCenterHorizontal = transform.position + new Vector3(collider.size.y / 2, 0, 0);
        RayPositionHighHorizontal = transform.position + new Vector3(collider.size.y / 2, RayPositionOffset, 0);
        RayPositionLowHorizontal = transform.position + new Vector3(collider.size.y / 2, -RayPositionOffset, 0);

        // Send out rays
        CeilingHitsCenter = Physics2D.RaycastAll(RayPositionCenterVertical, Vector2.up, RayLength);
        CeilingHitsLeft = Physics2D.RaycastAll(RayPositionLeftVertical, Vector2.up, RayLength);
        CeilingHitsRight = Physics2D.RaycastAll(RayPositionRightVertical, Vector2.up, RayLength);

        WallHitsCenter = Physics2D.RaycastAll(RayPositionCenterHorizontal, Vector2.right, RayLength);
        WallHitsLow = Physics2D.RaycastAll(RayPositionLowHorizontal, Vector2.right, RayLength);
        WallHitsHigh = Physics2D.RaycastAll(RayPositionHighHorizontal, Vector2.right, RayLength);

        // Save hits
        AllRaycastHits[0] = CeilingHitsCenter;
        AllRaycastHits[1] = CeilingHitsLeft;
        AllRaycastHits[2] = CeilingHitsRight;
        AllRaycastHits[3] = WallHitsCenter;
        AllRaycastHits[4] = WallHitsHigh;
        AllRaycastHits[5] = WallHitsLow;


        // Debug
        Debug.DrawRay(RayPositionCenterVertical, Vector2.up * RayLength, Color.red);
        Debug.DrawRay(RayPositionLeftVertical, Vector2.up * RayLength, Color.red);
        Debug.DrawRay(RayPositionRightVertical, Vector2.up * RayLength, Color.red);
        Debug.DrawRay(RayPositionCenterHorizontal, Vector2.right * RayLength, Color.red);
        Debug.DrawRay(RayPositionLowHorizontal, Vector2.right * RayLength, Color.red);
        Debug.DrawRay(RayPositionHighHorizontal, Vector2.right * RayLength, Color.red);

        if (shouldDie(AllRaycastHits))
        {
            Debug.Log("kill wall");
            Death();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
            DeathControl();
    }
}
