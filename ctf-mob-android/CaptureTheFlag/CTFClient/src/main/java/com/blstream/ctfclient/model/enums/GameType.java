package com.blstream.ctfclient.model.enums;

public enum GameType {
    FRAGS(0), TIME(1);

    private int type;

    GameType(int value) {
        this.type = value;
    }

    public static String getGameTypeNameForValue(Object value) {
        GameType[] values = values();
        String enumValue = null;
        for (GameType eachValue : values) {
            enumValue = eachValue.toString();
            if (enumValue.equals(value)) {
                return eachValue.name();
            }
        }
        return enumValue;
    }

    public int getType() {
        return type;
    }
}
