using UnityEngine;

namespace com.terranovita.botretreat
{

    public class BotController : MonoBehaviour, IBotDependant
    {

        public class OrientationVector
        {
            public static Vector3 NORTH = new Vector3(0f, 0f, 1f);
            public static Vector3 EAST = new Vector3(1f, 0f, 0f);
            public static Vector3 SOUTH = new Vector3(0f, 0f, -1f);
            public static Vector3 WEST = new Vector3(-1f, 0f, 0f);

            public static Vector3 createFrom(Orientation orientation)
            {
                switch (orientation)
                {
                    case Orientation.North:
                        return NORTH;
                    case Orientation.East:
                        return EAST;
                    case Orientation.South:
                        return SOUTH;
                    case Orientation.West:
                        return WEST;
                }
                return Vector3.zero;
            }
        }

        private Bot _bot;

        public float speed = 2;
        public float rotationSpeed = 20;

        public IBotDependant NameTagController { get; set; }
        public IBotDependant HealthController { get; set; }
        public IBotDependant StaminaController { get; set; }


        private Animation anim;

        /*
        private string[] loops=["loop_idle", "loop_run_funny", "loop_walk_funny"];
        private string[] combos=["cmb_street_fight"];
        private string[] kick=[ "kick_jump_right", "kick_lo_right"];
        private string[] punch=["punch_hi_left", "punch_hi_right"];
        private string[] rest=["def_head", "final_head", "jump",  "xhit_body", "xhit_head"];
        */


        void Start()
        {
            anim = this.gameObject.GetComponentInChildren<Animation>();
            if (anim != null)
            {
                anim["loop_run_funny"].speed = 4.0f;
            }
            instantRefresh();
        }

        public void instantRefresh()
        {
            if (_bot != null)
            {
                transform.position = GridController.Instance.gridToWorldPosition(_bot.LocationX, _bot.LocationY);
                transform.eulerAngles = OrientationVector.createFrom(_bot.Orientation);
            }
        }

        void Update()
        {
            if (_bot != null)
            {
                float step = speed * Time.deltaTime;
                Vector3 targetWorldPosition = GridController.Instance.gridToWorldPosition(_bot.LocationX, _bot.LocationY);
                Vector3 newPos = Vector3.MoveTowards(transform.position, targetWorldPosition, step);
                if ((newPos - transform.position).magnitude < 0.01)
                {
                    //Debug.Log((newPos - transform.position).magnitude);
                    GoAnim("loop_idle");
                }
                else {
                    GoAnim("loop_run_funny");
                }
                transform.position = newPos;

                Vector3 targetDir = OrientationVector.createFrom(_bot.Orientation);
                float rotationStep = rotationSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0.0F);
                Debug.DrawRay(transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }

        void GoAnim(string nme)
        {
            if (!anim.IsPlaying(nme))
            {
                //Debug.Log(anim.clip.name +" vs "+ nme);
                anim.Stop();
                anim.Play(nme);
            }
        }

        public void UpdateBot(Bot bot)
        {
            _bot = bot;
            NameTagController.UpdateBot(bot);
            HealthController.UpdateBot(bot);
            StaminaController.UpdateBot(bot);
        }

        public void Destroy()
        {
            NameTagController.Destroy();
            HealthController.Destroy();
            StaminaController.Destroy();
            Destroy(gameObject);
            Destroy(this);
        }
    }
}