import { useRef } from "react"
import { Message } from "../../types/message"
import AddMessage from "../AddMessoge/AddMessage"
import MessageCom from "../MessageCom/MessageCom"


export default function User() {

    const listApiRef=useRef<Message[]>()
    //מכילה 2 קומפוננטות ומאפשרת העברת נתונים בינהם
      return (
        <div className="user">
          <div className="row">
            <div className="col" ><MessageCom ref={listApiRef}></MessageCom></div>
            <div className="col"><AddMessage listApiRef={listApiRef}></AddMessage></div>
          </div>
        </div>
      )
    
}
