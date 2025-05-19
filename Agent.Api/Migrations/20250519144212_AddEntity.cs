using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agent.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_conversation_members_conversations_conversation_id",
                table: "conversation_members");

            migrationBuilder.DropForeignKey(
                name: "fk_conversation_members_users_user_id",
                table: "conversation_members");

            migrationBuilder.DropForeignKey(
                name: "fk_conversations_users_created_by_id",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_conversations_conversation_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_sender_id",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_messages",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_conversations",
                table: "conversations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_conversation_members",
                table: "conversation_members");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "conversations",
                newName: "Conversations");

            migrationBuilder.RenameTable(
                name: "conversation_members",
                newName: "ConversationMembers");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "employee_code",
                table: "Users",
                newName: "EmployeeCode");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "Users",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "avatar_url",
                table: "Users",
                newName: "AvatarUrl");

            migrationBuilder.RenameIndex(
                name: "ix_users_employee_code",
                table: "Users",
                newName: "IX_Users_EmployeeCode");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Messages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Messages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Messages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "conversation_id",
                table: "Messages",
                newName: "ConversationId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_sender_id",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_conversation_id",
                table: "Messages",
                newName: "IX_Messages_ConversationId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Conversations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Conversations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_group",
                table: "Conversations",
                newName: "IsGroup");

            migrationBuilder.RenameColumn(
                name: "created_by_id",
                table: "Conversations",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Conversations",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_conversations_created_by_id",
                table: "Conversations",
                newName: "IX_Conversations_CreatedById");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ConversationMembers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "ConversationMembers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "joined_at",
                table: "ConversationMembers",
                newName: "JoinedAt");

            migrationBuilder.RenameColumn(
                name: "is_admin",
                table: "ConversationMembers",
                newName: "IsAdmin");

            migrationBuilder.RenameColumn(
                name: "conversation_id",
                table: "ConversationMembers",
                newName: "ConversationId");

            migrationBuilder.RenameIndex(
                name: "ix_conversation_members_user_id",
                table: "ConversationMembers",
                newName: "IX_ConversationMembers_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_conversation_members_conversation_id_user_id",
                table: "ConversationMembers",
                newName: "IX_ConversationMembers_ConversationId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationMembers",
                table: "ConversationMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMembers_Conversations_ConversationId",
                table: "ConversationMembers",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMembers_Users_UserId",
                table: "ConversationMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_CreatedById",
                table: "Conversations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMembers_Conversations_ConversationId",
                table: "ConversationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMembers_Users_UserId",
                table: "ConversationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_CreatedById",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationMembers",
                table: "ConversationMembers");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "conversations");

            migrationBuilder.RenameTable(
                name: "ConversationMembers",
                newName: "conversation_members");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "EmployeeCode",
                table: "users",
                newName: "employee_code");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "users",
                newName: "display_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "users",
                newName: "avatar_url");

            migrationBuilder.RenameIndex(
                name: "IX_Users_EmployeeCode",
                table: "users",
                newName: "ix_users_employee_code");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "messages",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "messages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "messages",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "messages",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "messages",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "messages",
                newName: "conversation_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "messages",
                newName: "ix_messages_sender_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationId",
                table: "messages",
                newName: "ix_messages_conversation_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "conversations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "conversations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsGroup",
                table: "conversations",
                newName: "is_group");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "conversations",
                newName: "created_by_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "conversations",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_CreatedById",
                table: "conversations",
                newName: "ix_conversations_created_by_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "conversation_members",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "conversation_members",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "JoinedAt",
                table: "conversation_members",
                newName: "joined_at");

            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "conversation_members",
                newName: "is_admin");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "conversation_members",
                newName: "conversation_id");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationMembers_UserId",
                table: "conversation_members",
                newName: "ix_conversation_members_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationMembers_ConversationId_UserId",
                table: "conversation_members",
                newName: "ix_conversation_members_conversation_id_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_messages",
                table: "messages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_conversations",
                table: "conversations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_conversation_members",
                table: "conversation_members",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_conversation_members_conversations_conversation_id",
                table: "conversation_members",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_conversation_members_users_user_id",
                table: "conversation_members",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_conversations_users_created_by_id",
                table: "conversations",
                column: "created_by_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_conversations_conversation_id",
                table: "messages",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_sender_id",
                table: "messages",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
