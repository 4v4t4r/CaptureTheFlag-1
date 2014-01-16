package com.blstream.ctfclient.constants;

/**
 * Created by Rafal on 14.01.14.
 */
public class CTFConstants {
	public static final String CLIENT_ID_PARAM_NAME = "client_id";
	public static final String CLIENT_ID = "94f5481b9ac440230663";
	public static final String CLIENT_SECRET_PARAM_NAME = "client_secret";
	public static final String CLIENT_SECRET = "660e440fbe257bcfaadea794acd41230c18fe4d0";
	public static final String GRANT_TYPE_PARAM_NAME = "grant_type";
	public static final String SCOPE_PARAM_NAME = "scope";
	public static final String USERNAME_PARAM_NAME = "username";
	public static final String PASSWORD_PARAM_NAME = "password";

	public static final String PROTOCOL = "http";
	public static final String HOST = "78.133.154.39";
	public static final int PORT = 8888;

	public static final String ACCESS_TOKEN_KEY_NAME = "access_token";


	public enum GrantType{

		PASSWORD("password");

		private String type;

		GrantType(String type){
			this.type = type;
		}

		public String getValue(){return type;}
	}

	public enum Scope{
		READ_AND_WRITE("read+write"),
		READ("read"),
		WRITE("write");

		private String value;

		Scope(String value){
			this.value = value;
		}

		public String getValue(){
			return value;
		}
	}
}
