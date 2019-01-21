using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO look at this later
using Theseus.Character;

public class RoomManager : MonoBehaviour {

    public static RoomManager instance = null;
	public List<RoomGeneration> allRooms = new List<RoomGeneration>();
    enum TileSet {Fabrication, Terraforming, Disposal, Purification, Security, Medical};
	public bool preset;
	[SerializeField] GameObject room, player, userInterface, questController, questMarker;
	
    public int roomCap;
	public static bool roomsLoaded, questLoaded, gameSetupComplete;
	static Queue<RoomGeneration> roomQueue = new Queue<RoomGeneration>();
    public GameObject[] spawnConfigs;
    public GameObject configContainer;

    //Objects to Spawn
    public WallHealth internalWall;
    public EnemySpawner enemySpawner;
    public TurretAI turretAI;
    public Pillar pillar;
    public HoleCollisions hole;
    public DropSpawner dropSpawner;
    public WinConditionTEMP wCondition;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		StartCoroutine(GameSetup());
	}

    IEnumerator GameSetup()
    {
		// Start UI
		gameSetupComplete = false;
		Instantiate(userInterface);
        // Pause Game
        GameManager.GamePause(true);
        // If not the hub start spawning Rooms;
        if (!preset)
        {
            SpawnFirstRoom();
        }
        else
        {
            roomsLoaded = true;
        }
        // Until the rooms are loaded wait
        while (!roomsLoaded) {
            yield return null;
        }
        // questLoaded being true turns off the time stop

        Instantiate(questController);
        QuestController.PullQuest();

        if (!preset)
        {
            QuestSiteSetup();
        }
        questLoaded = true;

        while (!questLoaded) {
			yield return null;
		}
		// Disable all rooms but the first one.
		bool first = false;
            foreach (RoomGeneration room in allRooms)
            {
                if (first)
                {
                    room.SetActive(false);
                }
                else
                {
                    first = true;
                }
            }
		if(FindObjectOfType<MinimapContoller>())
			FindObjectOfType<MinimapContoller>().GetRoomPos();
        // Game Unpause
        gameSetupComplete = true;
		GameManager.GamePause(false);
	}
	
	// Update is called once per frame

    void SpawnFirstRoom()
    {
		roomsLoaded = false;
		RoomGeneration.roomsInExistence = 0;
        RoomGeneration.spawncap = 0;
		RoomGeneration.first = true;
        Instantiate(room, Vector3.zero, Quaternion.identity);
        StartCoroutine(ManageRoomQueue());
    }

    IEnumerator ManageRoomQueue()
    {
		yield return new WaitForFixedUpdate();
		while (RoomGeneration.spawncap < roomCap-1)
		{
			if (roomQueue.Count > 0)
			{
				RoomGeneration thisRoom = roomQueue.Peek();				
				thisRoom.QueuedStart();
				roomQueue.Dequeue();				
			}			
			yield return null;
		}
		//RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		foreach(RoomGeneration room in allRooms)
		{
			DoorGen[] doors = room.GetComponentsInChildren<DoorGen>();
			foreach (DoorGen door in doors)
			{
				door.EndSpawningCheck();
                roomQueue.Clear();
			}
		}
		CheckAllActiveRooms();
		Invoke("CheckAllActiveRooms", 0.25f);
		roomsLoaded = true;
	}

	private void CheckAllActiveRooms()
	{
		foreach (RoomGeneration r in allRooms)
		{
			r.ToggleActiveRooms();
		}
	}

	public void AdditionalRoom(RoomGeneration room)
	{
		roomQueue.Enqueue(room);
		allRooms.Add(room);
	}

    public GameObject GetRandomRoom()
    {
        int layoutNumber = Random.Range(0, spawnConfigs.Length -1);
        return spawnConfigs[layoutNumber];
    }

	public static GameObject GetCurrentActiveRoom()
	{
		//RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		foreach (RoomGeneration room in RoomManager.instance.allRooms)
		{
			if (room.GetActive())
			{
				return room.gameObject;
			}
		}
		Debug.LogWarning("No active room");
		return null;
	}

    void QuestSiteSetup()
    {
        RoomGeneration questSpawnSite;
        List<RoomGeneration> questSpawnOptions = new List<RoomGeneration>();

        foreach (spawnFunc cfg in QuestController.availConfigs)
        {
            if (cfg.GetComponent<AllConfig>().configNumber == 23 || cfg.GetComponent<AllConfig>().configNumber == 9 || cfg.GetComponent<AllConfig>().configNumber == 4)
            {
                questSpawnOptions.Add(cfg.GetComponentInParent<RoomGeneration>());
            }

        }
        if(questSpawnOptions.Count > 0)
        {
            questSpawnSite = questSpawnOptions[Random.Range(0, questSpawnOptions.Count)];
            print(questSpawnSite.name + " has a quest");
            Instantiate(questMarker, questSpawnSite.transform);
        }

            print("Quest loading is complete if applicable");

    }

    //Track number of rooms for special rooms
}
