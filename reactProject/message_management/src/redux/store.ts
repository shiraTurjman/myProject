
import rootReducer from './reducers/rootReducer';
import {createStore} from  'redux';
import { devToolsEnhancer } from "redux-devtools-extension";


const store= createStore(rootReducer,devToolsEnhancer({}))



export default store;