using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabsGroup : MonoBehaviour
{
    [SerializeField]
    private RectTransform tabsContainer;
    [SerializeField]
    private RectTransform pagesContainer;

    [SerializeField]
    private List<Tab> tabs; // Las pestañas
    public List<GameObject> pages; // Las páginas con su contenido
    public Tab selectedTab; // La pestaña seleccionada
    [SerializeField]
    private RectTransform underline; // La linea de decoración que marca la página seleccionada
    public Color selectedColor; // El color de cuando está seleccionada
    public Color deselectedColor; // El color de no está seleccionada

    [SerializeField]
    private float underlineMoveTime = 0.5f;
    [SerializeField]
    private LeanTweenType underlineTransition;


    public void SelectTab(Tab tab)
    {
        tab.SetColor(selectedColor);
        // Desactivamos las páginas
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        // Desactivamos las pestañas excepto la seleccionada
        foreach (Tab _tab in tabs)
        {
            if(_tab.transform.GetSiblingIndex() != tab.transform.GetSiblingIndex())
                _tab.SetColor(deselectedColor);
        }
        // Cambiamos la pestaña seleccionada
        selectedTab = tab;
        // Activamos la pagina seleccionada
        pages[tab.transform.GetSiblingIndex()].SetActive(true);
        // Ponemos la underline debajo de la pestaña
        LeanTween.moveX(underline, tab.GetComponent<RectTransform>().anchoredPosition.x, underlineMoveTime).setEase(underlineTransition);
    }

    public GameObject CreateTab(GameObject tab, GameObject page, string tabName) {
        Tab createdtab = Instantiate(tab, tabsContainer).GetComponent<Tab>();

        createdtab.tabsGroup = this;
        createdtab.transform.SetSiblingIndex(createdtab.transform.GetSiblingIndex() - 1);
        createdtab.Initialize(deselectedColor, tabName);

        page = Instantiate(page, pagesContainer);
        page.SetActive(false);

        tabs.Add(createdtab);
        pages.Add(page);

        return page;
    }

    public void DeleteTab(int tabIndex) {
        // Manejamos lo que pasa si está seleccionada
        if (tabs[tabIndex].transform.GetSiblingIndex() == selectedTab.transform.GetSiblingIndex()) {
            if(selectedTab.transform.GetSiblingIndex() > 0) SelectTab(tabs[tabIndex - 1]);
            else {
                selectedTab = null;
            }
        }

        // Destruimos la pestaña y la página
        Destroy(tabs[tabIndex].gameObject);
        Destroy(pages[tabIndex]);

        // Las eliminamos de la lista
        tabs.RemoveAt(tabIndex);
        pages.RemoveAt(tabIndex);
    }

    public void DeleteLastTab() {
        DeleteTab(tabs.Count - 1); // Borra la última pestaña
    }

    public int GetTabsLenght() {
        return tabs.Count;
    }
}