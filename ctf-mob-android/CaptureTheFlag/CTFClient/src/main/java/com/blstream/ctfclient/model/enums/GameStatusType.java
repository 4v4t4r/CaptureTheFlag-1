package com.blstream.ctfclient.model.enums;

public enum GameStatusType {
    IN_PROGRESS(0), CREATED(1), ON_HOLD(2), CANCELED(3);

    private int type;

    GameStatusType(int value) {
        this.type = value;
    }

    public static String getGameStatusTypeNameForValue(Object value) {
        GameStatusType[] values = values();
        String enumValue = null;
        for (GameStatusType eachValue : values) {
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
