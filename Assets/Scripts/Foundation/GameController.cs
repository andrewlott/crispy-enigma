using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : BaseController {
    private static GameController _instance;
    public static GameController Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.Find("GameController").GetComponent<GameController>();
            }
            return _instance;
        }
    }

    void Start() {
        // Add systems here
        TouchSystem ts = new TouchSystem();
        AddSystem(ts);

        AnimationSystem ans = new AnimationSystem();
        AddSystem(ans);
        UISystem uis = new UISystem();
        AddSystem(uis);
        PauseSystem ps = new PauseSystem();
        AddSystem(ps);
        DestroySystem ds = new DestroySystem();
        AddSystem(ds);

        AdSystem ads = new AdSystem();
        AddSystem(ads);

        Enable();
        ExtraSetup();
    }

    private void ExtraSetup() {
        GameObject debug = GameObject.Find("Debug");
        if (debug != null) {
#if UNITY_EDITOR
            debug.SetActive(true);
#else
			debug.SetActive(false);
#endif
        }
    }

    public void Restart() {
        Disable();
    }

    public override void OnUpdate() {

    }

    public void StartGame() {
        Enable();
    }

    public void NewGame() {
        Restart();
        StartGame();
    }

    public void EndGame() {

    }

    public void Pause() {
        PauseComponent pc = gameObject.GetComponent<PauseComponent>();
        if (pc != null) {
            Destroy(pc);
        } else {
            gameObject.AddComponent<PauseComponent>();
        }
    }

    public void OnBack() {
        Destroy(Pool.Instance.ComponentForType(typeof(AdComponent)));

    }
}