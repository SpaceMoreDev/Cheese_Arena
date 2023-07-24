using System;
using UnityEngine;

namespace Behaviours
{
    public class Dodge
    {
        private Animator _anim;
        private CharacterController _controller;
        private Camera _camera;
        private Movement _movement;
        private float dodgeSpeed = 2f;
        private bool _isdodging = false;
        public bool IsDodging {get{return _isdodging;}}
        public event Action FinishDodge;

        //constants
        const string PLAYER_DODGE = "Dodge";

        public Dodge(Animator animator, Movement movement, CharacterController controller, float speed)
        {
            _anim = animator;
            _movement = movement;
            _controller = controller;
            dodgeSpeed = speed;
        }
        public Dodge(Animator animator, Movement movement, CharacterController controller, float speed, Camera camera)
        {
            _anim = animator;
            _movement = movement;
            _controller = controller;
            _camera = camera;
            dodgeSpeed = speed;
        }

        public void StartDodge()
        {
            _anim.SetBool("isDodging", false);
            if(!_isdodging)
            {
                // staminabar.DecreaseStamina(dodgeStamina);

                _anim.SetBool("Shield",false);
                _anim.SetBool("Attack", false);

                if(!_anim.GetBool("isDodging"))
                {
                    _anim.SetBool("isDodging", true);
                    _anim.Play(PLAYER_DODGE,0);
                }
                _movement.CanMove = false;
                
            }
        }
        
        public void Dodging()
        {   
            if(_controller.velocity != Vector3.zero){
                Vector3 direction = new Vector3(_controller.velocity.x, 0, _controller.velocity.y);
                if(_camera != null)
                    direction = direction.x * _camera.transform.right.normalized + direction.z * _camera.transform.forward.normalized;
                else
                    direction = direction.x * _controller.transform.right.normalized + direction.z * _controller.transform.forward.normalized;
                
                direction.y = 0f;
                _controller.gameObject.transform.forward = direction;
            }

            Vector3 movementVelocity = _controller.gameObject.transform.forward * dodgeSpeed;
            _movement.SetVelocity(new Vector3(movementVelocity.x,3,movementVelocity.z));
            _isdodging = true;
        }
        
        public void DodgeEnd()
        {
            // FinishDodge?.Invoke();
            _movement.SetVelocity(new Vector3(0,_movement.CurrentVelocity.y,0));
            _anim.SetBool("isDodging", false);
            _movement.CanMove = true;
            _isdodging = false;
        }
    }
}
