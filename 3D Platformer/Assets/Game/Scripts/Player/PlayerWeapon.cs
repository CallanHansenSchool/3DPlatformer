using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    private const KeyCode AIM_BUTTON = KeyCode.Mouse1;
    private const KeyCode SHOOT_BUTTON = KeyCode.Mouse0;

    public bool Aiming = false;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectileSpawnPosition = null;
    [SerializeField] private float projectileSpeed = 4;

    [SerializeField] private GameObject crosshair = null;
    public static PlayerWeapon Instance;

    [SerializeField] private float rayGetPoint = 250;

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void Start()
    {
        crosshair.SetActive(false);
    }

    void Update()
    {     
        if (PlayerManager.Instance.PlayerMovement.TouchingOverallGround)
        {
            if(!PauseMenu.Instance.Paused)
            {
                if (Input.GetKeyDown(AIM_BUTTON))
                {
                    Aiming = true;
                    CheckAim();
                }

                if (Input.GetKeyUp(AIM_BUTTON))
                {
                    Aiming = false;
                    CheckAim();
                }

                if (Input.GetKeyDown(SHOOT_BUTTON))
                {
                    if (Aiming)
                    {
                        ShootSpikes();
                        CheckAim();
                    }
                }
            }          
        }

        if(Aiming)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            PlayerManager.Instance.PlayerMovement.enabled = false;
        }
       
        PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.AIMING, Aiming);
    }

    void CheckAim() // Check whether the player is aiming or not
    {
        if (Aiming)
        {
            PlayerManager.Instance.PlayerMovement.enabled = false;        
            crosshair.SetActive(true);
            GameManager.Instance.aimingCamera.Priority = 1;
            GameManager.Instance.MainVirtualCamera.Priority = 0;
        }
        else
        {
            PlayerManager.Instance.PlayerMovement.enabled = true;
            crosshair.SetActive(false);
            GameManager.Instance.aimingCamera.Priority = 0;
            GameManager.Instance.MainVirtualCamera.Priority = 1;
        }
    }

    public void ShootSpikes()
    {
        // Create an invisible ray from the camera going through the middle of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Check whether the ray is pointing to something so it can adjust the direction
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }        
        else
        {
            targetPoint = ray.GetPoint(rayGetPoint); 
        }
        
        GameObject projectile = Instantiate<GameObject>(projectilePrefab, projectileSpawnPosition.position, Quaternion.Euler(projectileSpawnPosition.rotation.x - 60, projectileSpawnPosition.rotation.y, projectileSpawnPosition.rotation.z)); // Spawn in the projectile
        projectile.GetComponent<Rigidbody>().velocity = (targetPoint - projectileSpawnPosition.transform.position).normalized * projectileSpeed; // Shoot the projectile in the correct direction

        PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.SHOOT);
    }
}
