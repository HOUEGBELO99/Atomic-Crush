using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class JoystickPlayerController : MonoBehaviour
{
    public Joystick joystick;
    public float inputDeadZone = 0.1f;
    public bool useCrouchButton;
    public bool useJumpButton;

    private ThirdPersonCharacter m_Character;
    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    private bool m_Jump;
    private bool m_Crouch;
    private Animator m_Animator;
    
    private void Start()
    {
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_Animator = GetComponent<Animator>();
        
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        if (joystick == null) return;

        // Récupérer les entrées du joystick
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        // Appliquer une zone morte pour éviter les micro-mouvements
        if (Mathf.Abs(h) < inputDeadZone) h = 0;
        if (Mathf.Abs(v) < inputDeadZone) v = 0;

        // Calculer la direction du mouvement par rapport à la caméra
        if (m_Cam != null)
        {
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        // Normaliser le mouvement si nécessaire
        if (m_Move.magnitude > 1f)
        {
            m_Move.Normalize();
        }

        // Appliquer le mouvement avec les états de saut et d'accroupissement
        m_Character.Move(m_Move, m_Crouch, m_Jump);

        // Réinitialiser le saut après l'avoir utilisé
        m_Jump = false;
    }

    // Pour le bouton de saut
    public void OnJumpPressed()
    {
        if (useJumpButton)
        {
            m_Jump = true;
        }
    }

    // Pour le bouton d'accroupissement
    public void OnCrouchPressed()
    {
        if (useCrouchButton)
        {
            m_Crouch = !m_Crouch;
        }
    }

    // Pour désactiver l'accroupissement
    public void OnCrouchReleased()
    {
        if (useCrouchButton)
        {
            m_Crouch = false;
        }
    }
}