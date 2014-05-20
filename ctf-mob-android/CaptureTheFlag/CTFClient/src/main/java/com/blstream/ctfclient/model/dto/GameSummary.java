package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

/**
 * Created by wde on 2014-05-14.
 */
public class GameSummary {
    @SerializedName("red_team_points")
    private int redTeamPoints;
    @SerializedName("blue_team_points")
    private int blueTeamPoints;
    private int status;
    @SerializedName("time_to_end")
    private String timeToEnd;

    public String getTimeToEnd() {
        return timeToEnd;
    }

    public int getRedTeamPoints() {
        return redTeamPoints;
    }

    public int getBlueTeamPoints() {
        return blueTeamPoints;
    }

    public int getStatus() {
        return status;
    }

    @Override
    public String toString() {
        return "GameSummary{" +
                "redTeamPoints=" + redTeamPoints +
                ", blueTeamPoints=" + blueTeamPoints +
                ", status=" + status +
                ", timeToEnd='" + timeToEnd + '\'' +
                '}';
    }
}