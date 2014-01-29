package com.blstream.ctfclient.model.enums;

public enum ItemType {
    RED_FLAG(0), BLUE_FLAG(1), RED_BASE(2), BLUE_BASE(3), MEDIC_BOX(4), PISTOL(5), AMMO(6);

    private int type;

    ItemType(int value) {
        this.type = value;
    }

    public static String getItemTypeNameForValue(Object value) {
        ItemType[] values = values();
        String enumValue = null;
        for (ItemType eachValue : values) {
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
