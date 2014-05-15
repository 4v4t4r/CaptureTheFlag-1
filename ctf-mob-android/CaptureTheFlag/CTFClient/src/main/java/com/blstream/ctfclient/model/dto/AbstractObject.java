package com.blstream.ctfclient.model.dto;

/**
 * Created by mar on 29.04.14.
 */
public abstract class AbstractObject {

    private String url;
    private String name;
    private String description;

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public long getId() {
        String tab[] = getUrl().split("/");
        return Long.parseLong(tab[tab.length - 1]);
    }
}
