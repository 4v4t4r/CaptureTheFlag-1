package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by wde on 29.01.14.
 */
public class Map {
    @SerializedName("id")
    String id;
    @SerializedName("name")
    String name;
    @SerializedName("description")
    String description;
    @SerializedName("location")
    float[] location;
    @SerializedName("radius")
    float radius;
    @SerializedName("created_by")
    String created_by;
    @SerializedName("created_date")
    Date created_date;
    @SerializedName("modified_date")
    Date modified_date;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
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

    public float[] getLocation() {
        return location;
    }

    public void setLocation(float[] location) {
        this.location = location;
    }

    public float getRadius() {
        return radius;
    }

    public void setRadius(float radius) {
        this.radius = radius;
    }

    public String getCreatedBy() {
        return created_by;
    }

    public void setCreatedBy(String created_by) {
        this.created_by = created_by;
    }

    public Date getCreatedDate() {
        return created_date;
    }

    public void setCreatedDate(Date created_date) {
        this.created_date = created_date;
    }

    public Date getModifiedDate() {
        return modified_date;
    }

    public void setModifiedDate(Date modified_date) {
        this.modified_date = modified_date;
    }
}
