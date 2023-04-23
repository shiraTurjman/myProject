import React, { useState, useEffect } from "react";

export default function LocalTry() {
  const [count, setCount] = useState(0);

  useEffect(() => {
    debugger;
    const storedCount = localStorage.getItem("count");
    if (storedCount !== null) {
      setCount(parseInt(storedCount));
    }
  }, []);

  useEffect(() => {
    debugger;
    localStorage.setItem("count", count.toString());
  }, [count]);

  return (
    <div>
      <p>Count: {count}</p>
      <button onClick={() => setCount(count + 1)}>Increment</button>
    </div>
  );
}