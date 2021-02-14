import React, { useContext, useEffect } from "react";
import styled from "styled-components";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";
import { UserDetailContext } from "../../context/userDetails";

const BoardContainer = styled.div``;

export default function Board() {
  const { userDetail } = useContext(UserDetailContext);

  if (!userDetail.states) {
    return null;
  }
  console.log(userDetail, userDetail.states);
  return (
    <BoardContainer>
      {userDetail.states.map((state) => (
        <div key={state.id}>{state.name}</div>
      ))}
    </BoardContainer>
  );
}
