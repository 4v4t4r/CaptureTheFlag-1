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

    public void setTimeToEnd(String timeToEnd) {
        this.timeToEnd = timeToEnd;
    }

    public int getRedTeamPoints() {
        return redTeamPoints;
    }

    public void setRedTeamPoints(int redTeamPoints) {
        this.redTeamPoints = redTeamPoints;
    }

    public int getBlueTeamPoints() {
        return blueTeamPoints;
    }

    public void setBlueTeamPoints(int blueTeamPoints) {
        this.blueTeamPoints = blueTeamPoints;
    }

    public int getStatus() {
        return status;
    }

    public void setStatus(int status) {
        this.status = status;
    }
}
