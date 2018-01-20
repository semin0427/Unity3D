using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ScrollControl : MonoBehaviour {

    public Texture2D[] availableIcons;
    public RectTransform prefab;
    public Text countText;
    public ScrollRect scrollView;
    public RectTransform content;
    StageManager SM;
    Scrollbar SB;

    List<ItemView> views = new List<ItemView>();
	// Use this for initialization
	void Start () {
        SB = GetComponentInChildren<Scrollbar>();
        SB.value = 1;
        SM = GameObject.Find("QuestManager").GetComponent<StageManager>();

        GameObject.Find("Content/Work").GetComponent<Image>().enabled = false;
        GameObject.Find("Content/Fix").GetComponent<Image>().enabled = false;
    }
	
    public void UpdateItems()
    {
        int newCount = 0;
        int.TryParse(countText.text, out newCount);

        //StartCoroutine(FetchItemModles(newCount, results => OnReceivedNewModel(results)));
        FetchItemModles(newCount, results => OnReceivedNewModel(results));
        Debug.Log("Dsadas");
    }

    void OnReceivedNewModel(ItemModel[] models)
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        int i = 0;

        foreach(var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            var view = InitializeItemView(instance, model, i);
            views.Add(view);
            ++i;
        }
    }

    ItemView InitializeItemView(GameObject viewGameObject, ItemModel model, int itemIndex)
    {
        ItemView view = new ItemView(viewGameObject.transform);

        view.titleText.text = model.Name;
        view.iconImage.texture = availableIcons[model.icon];

        return view;
    }

    void FetchItemModles(int count, Action<ItemModel[]> onDone)
    {
        //yield return new WaitForSeconds(2f);

        var results = new ItemModel[count];
        for(int i = 0; i<count; ++i)
        {
            results[i] = new ItemModel();
            results[i].Name = "Item " + i;
            results[i].icon = UnityEngine.Random.Range(0, availableIcons.Length);
        }
    }

    public class ItemView
    {
        public Text titleText;
        public RawImage iconImage;

        public ItemView(Transform rootView)
        {
            titleText = rootView.Find("Cost").GetComponent<Text>();
            iconImage = rootView.Find("ClassIcon").GetComponent<RawImage>();
        }

    }

	// Update is called once per frame
	void Update () {
        tutorialUI();
    }

    public class ItemModel
    {
        public string Name;
        public int icon;
    }

    public void tutorialUI()
    {
        if (Application.loadedLevel == 2)
        {
            if (SM.tutorialNum >= 4)
                GameObject.Find("Content/Sword").GetComponent<Image>().enabled = false;

            if (SM.tutorialNum == 10)
                GameObject.Find("Content/Work").GetComponent<Image>().enabled = true;
            else
                GameObject.Find("Content/Work").GetComponent<Image>().enabled = false;

            if(SM.tutorialNum == 12)
                GameObject.Find("Content/Fix").GetComponent<Image>().enabled = true;
            else
                GameObject.Find("Content/Fix").GetComponent<Image>().enabled = false;
        }
    }
}
