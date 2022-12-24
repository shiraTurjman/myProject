
import { combineReducers } from "redux";

import UserReducer from "./userReducer";

const rootReducer=combineReducers({
    
    UserReducer
})

//יצירת סוג חדש של מחלקה ראשית
//לפי הסוג שמחלקה ראשית מכיל
export type storeType=ReturnType<typeof rootReducer>;

export default rootReducer;