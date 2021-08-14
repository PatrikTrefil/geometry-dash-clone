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
    [SerializeField] private AudioClip clip;
    bool isDead = false;
    AudioSource audioSource;
    Animator animator;
    BoxCollider2D playerCollider;
    float rayPositionOffset;

    // one variable per ray
    // if we wanted to add more rays, we should move to arrays for storage
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
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        rayPositionOffset = (playerCollider.size.y / 2) - 0.2f;
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

    bool ShouldDie(RaycastHit2D[][] AllRaycastHits)
    {
        foreach (RaycastHit2D[] HitList in AllRaycastHits)
            foreach (RaycastHit2D hit in HitList)
                if (hit.collider != null)
                    if (hit.collider.CompareTag("Block"))
                        return true;
        return false;
    }

    void DeathControl()
    {
        // Calculate ray starting points
        RayPositionCenterVertical = transform.position + new Vector3(0, playerCollider.size.y / 2, 0);
        RayPositionLeftVertical = transform.position + new Vector3(-rayPositionOffset, playerCollider.size.y / 2, 0);
        RayPositionRightVertical = transform.position + new Vector3(rayPositionOffset, playerCollider.size.y / 2, 0);

        RayPositionCenterHorizontal = transform.position + new Vector3(playerCollider.size.y / 2, 0, 0);
        RayPositionHighHorizontal = transform.position + new Vector3(playerCollider.size.y / 2, rayPositionOffset, 0);
        RayPositionLowHorizontal = transform.position + new Vector3(playerCollider.size.y / 2, -rayPositionOffset, 0);

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

        if (ShouldDie(AllRaycastHits))
        {
            Debug.Log("killed by wall");
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
