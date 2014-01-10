package com.blstream.ctfclient.tests.model;

import com.blstream.ctfclient.RobolectricGradleTestRunner;
import com.blstream.ctfclient.model.dto.Character;
import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.enums.CharacterType;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;

import static org.junit.Assert.assertTrue;

/**
 * Created by Rafal on 10.01.14.
 */
@RunWith(RobolectricGradleTestRunner.class)
public class DTOTest {

	@Before
	public void setup() {
	}

	@Test
	public void testSerializationDTOObjects() {
		String expectedResult = "{\"username\":\"Rafal Zadrozny\",\"email\":\"zrafal86@gmail.com\",\"password\":\"haslo_rafala\",\"nick\":\"zrafal86\",\"location\":[52.0,57.0],\"characters\":[{\"type\":0,\"total_time\":0,\"total_score\":0,\"health\":1.0,\"level\":0}]}";

		Character character = new Character();
		character.setHealth(1);
		character.setLevel(0);
		character.setTotalScore(0);
		character.setTotalTime(0);
		character.setType(CharacterType.PRIVATE.getType());

		User user = new User();
		user.setCoordinates(52, 57);
		user.setEmail("zrafal86@gmail.com");
		user.setNick("zrafal86");
		user.setUserName("Rafal Zadrozny");
		user.setPassword("haslo_rafala");
		user.setCharacter(character);
		Gson gson = new GsonBuilder().create();
		String json = gson.toJson(user);

		String message = new StringBuilder("Is JSON: ").append(json).append("\n, but should be : ").append(expectedResult)
				.toString();
		assertTrue(message, expectedResult.equals(json));
	}

}