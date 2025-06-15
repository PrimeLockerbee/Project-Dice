using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public class MenuPanel
    {
        public string id;
        public GameObject panel;
    }

    public MenuPanel[] panels;

    private void Start()
    {
        ShowOnly("MainMenu"); // or "Setup" or whatever your starting panel id is
    }

    public void ShowOnly(string idToShow)
    {
        foreach (var menu in panels)
        {
            bool shouldBeActive = (menu.id == idToShow);
            menu.panel.SetActive(shouldBeActive);
        }
    }

    public void Toggle(string id)
    {
        foreach (var menu in panels)
        {
            if (menu.id == id)
                menu.panel.SetActive(!menu.panel.activeSelf);
        }
    }

    public void CloseAll()
    {
        ShowOnly(null);
    }

    public void ShowPanel(string id)
    {
        ShowOnly(id);
    }
}
